// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using Pomelo.Data.MySql;

namespace Pomelo.Data.Types
{
  /// <summary>
  /// Summary description for MySqlConversionException.
  /// </summary>
  
  public class MySqlConversionException : Exception
  {
    /// <summary>Ctor</summary>
    public MySqlConversionException(string msg)
      : base(msg)
    {
    }
  }
}
