// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;


namespace Pomelo.Data.MySql
{
  /// <summary>
  /// Summary description for MySqlPoolManager.
  /// </summary>
  internal class MySqlPoolManager
  {
    private static Dictionary<string, MySqlPool> pools = new Dictionary<string, MySqlPool>();
    private static List<MySqlPool> clearingPools = new List<MySqlPool>();

    // Timeout in seconds, after which an unused (idle) connection 
    // should be closed.
    static internal int maxConnectionIdleTime = 180;


    static MySqlPoolManager()
    {
#if !NETSTANDARD1_6
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(EnsureClearingPools);
      AppDomain.CurrentDomain.DomainUnload += new EventHandler(EnsureClearingPools);
#endif
        }

        private static void EnsureClearingPools( object sender, EventArgs e )
    {
      MySqlPoolManager.ClearAllPools();
    }

    // we add a small amount to the due time to let the cleanup detect
    //expired connections in the first cleanup.
    private static Timer timer = new Timer(new TimerCallback(CleanIdleConnections),
      null, (maxConnectionIdleTime * 1000) + 8000, maxConnectionIdleTime * 1000);
 
    private static string GetKey(MySqlConnectionStringBuilder settings)
    {
      string key = "";
      lock (settings)
      {
        key = settings.ConnectionString;
      }
#if !NETSTANDARD1_6
      if (settings.IntegratedSecurity && !settings.ConnectionReset)
      {
        try
        {
          // Append SID to the connection string to generate a key
          // With Integrated security different Windows users with the same
          // connection string may be mapped to different MySQL accounts.
          System.Security.Principal.WindowsIdentity id =
            System.Security.Principal.WindowsIdentity.GetCurrent();

          key += ";" + id.User;
        }
        catch (System.Security.SecurityException ex)
        {
          // Documentation for WindowsIdentity.GetCurrent() states 
          // SecurityException can be thrown. In this case the 
          // connection can only be pooled if reset is done.
          throw new MySqlException(Resources.NoWindowsIdentity, ex);
        }
      }
#endif
            return key;
    }
    public static MySqlPool GetPool(MySqlConnectionStringBuilder settings)
    {
      string text = GetKey(settings);

      lock (pools)
      {
        MySqlPool pool;
        pools.TryGetValue(text, out pool);

        if (pool == null)
        {
          pool = new MySqlPool(settings);
          pools.Add(text, pool);
        }
        else
          pool.Settings = settings;

        return pool;
      }
    }

    public static void RemoveConnection(Driver driver)
    {
      Debug.Assert(driver != null);

      MySqlPool pool = driver.Pool;
      if (pool == null) return;

      pool.RemoveConnection(driver);
    }

    public static void ReleaseConnection(Driver driver)
    {
      Debug.Assert(driver != null);

      MySqlPool pool = driver.Pool;
      if (pool == null) return;

      pool.ReleaseConnection(driver);
    }

    public static void ClearPool(MySqlConnectionStringBuilder settings)
    {
      Debug.Assert(settings != null);
      string text;
      try
      {
        text = GetKey(settings);
      }
      catch (MySqlException)
      {
        // Cannot retrieve windows identity for IntegratedSecurity=true
        // This can be ignored.
        return;
      }
      ClearPoolByText(text);
    }

    private static void ClearPoolByText(string key)
    {
      lock (pools)
      {
        // if pools doesn't have it, then this pool must already have been cleared
        if (!pools.ContainsKey(key)) return;

        // add the pool to our list of pools being cleared
        MySqlPool pool = (pools[key] as MySqlPool);
        clearingPools.Add(pool);

        // now tell the pool to clear itself
        pool.Clear();

        // and then remove the pool from the active pools list
        pools.Remove(key);
      }
    }

    public static void ClearAllPools()
    {
      lock (pools)
      {
        // Create separate keys list.
        List<string> keys = new List<string>(pools.Count);

        foreach (string key in pools.Keys)
          keys.Add(key);

        // Remove all pools by key.
        foreach (string key in keys)
          ClearPoolByText(key);
      }
    }

    public static void RemoveClearedPool(MySqlPool pool)
    {
      Debug.Assert(clearingPools.Contains(pool));
      clearingPools.Remove(pool);
    }

    /// <summary>
    /// Remove drivers that have been idle for too long.
    /// </summary>
    public static void CleanIdleConnections(object obj)
    {
      List<Driver> oldDrivers = new List<Driver>();
      lock (pools)
      {
        foreach (string key in pools.Keys)
        {
          MySqlPool pool = (pools[key] as MySqlPool);
          oldDrivers.AddRange(pool.RemoveOldIdleConnections());
        }
      }
      foreach (Driver driver in oldDrivers)
      {
        driver.Close();
      }
    }
  }
}