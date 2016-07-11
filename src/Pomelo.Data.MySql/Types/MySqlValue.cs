// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Data;
using System.Globalization;
using Pomelo.Data.MySql;

namespace Pomelo.Data.Types
{
  internal interface IMySqlValue
  {
    bool IsNull { get; }
    MySqlDbType MySqlDbType { get; }
    object Value { get; /*set;*/ }
    Type SystemType { get; }
    string MySqlTypeName { get; }

    void WriteValue(MySqlPacket packet, bool binary, object value, int length);
    IMySqlValue ReadValue(MySqlPacket packet, long length, bool isNull);
    void SkipValue(MySqlPacket packet);
  }
}
