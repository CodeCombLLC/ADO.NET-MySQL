// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;


namespace Pomelo.Data.MySql
{
  internal class PerformanceMonitor
  {
    
    public PerformanceMonitor(MySqlConnection connection)
    {
      Connection = connection;
    }

    public MySqlConnection Connection { get; private set; }

    public virtual void AddHardProcedureQuery()
    {
    }

    public virtual void AddSoftProcedureQuery()
    {
    }
  }
}