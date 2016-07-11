// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace Pomelo.Data.MySql.Authentication
{
    internal class AuthenticationPluginManager
    {
      static Dictionary<string, PluginInfo> plugins = new Dictionary<string, PluginInfo>();

      static AuthenticationPluginManager()
      {
        plugins["mysql_native_password"] = new PluginInfo("Pomelo.Data.MySql.Authentication.MySqlNativePasswordPlugin");
        plugins["sha256_password"] = new PluginInfo("Pomelo.Data.MySql.Authentication.Sha256AuthenticationPlugin");
#if !NETSTANDARD1_6
        plugins["authentication_windows_client"] = new PluginInfo("Pomelo.Data.MySql.Authentication.MySqlWindowsAuthenticationPlugin");
        if (MySqlConfiguration.Settings != null && MySqlConfiguration.Settings.AuthenticationPlugins != null)
        {
          foreach (AuthenticationPluginConfigurationElement e in MySqlConfiguration.Settings.AuthenticationPlugins)
            plugins[e.Name] = new PluginInfo(e.Type);
        }
#endif
        }

        public static MySqlAuthenticationPlugin GetPlugin(string method)
      {
        if (!plugins.ContainsKey(method))
          throw new MySqlException(String.Format(Resources.AuthenticationMethodNotSupported, method));
        return CreatePlugin(method);
      }

      private static MySqlAuthenticationPlugin CreatePlugin(string method)
      {
        PluginInfo pi = plugins[method];

        try
        {
          Type t = Type.GetType(pi.Type);
          MySqlAuthenticationPlugin o = (MySqlAuthenticationPlugin)Activator.CreateInstance(t);
          return o;
        }
        catch (Exception e)
        {
          throw new MySqlException(String.Format(Resources.UnableToCreateAuthPlugin, method), e);
        }
      }
    }

    struct PluginInfo
    {
      public string Type;
      public Assembly Assembly;

      public PluginInfo(string type)
      {
        Type = type;
        Assembly = null;
      }
    }
}
