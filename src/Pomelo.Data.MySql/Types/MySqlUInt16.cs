// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See License.txt in the project root for license information.

using System;
using System.Data;
using Pomelo.Data.MySql;

namespace Pomelo.Data.Types
{
  internal struct MySqlUInt16 : IMySqlValue
  {
    private ushort mValue;
    private bool isNull;

    public MySqlUInt16(bool isNull)
    {
      this.isNull = isNull;
      mValue = 0;
    }

    public MySqlUInt16(ushort val)
    {
      this.isNull = false;
      mValue = val;
    }

    #region IMySqlValue Members

    public bool IsNull
    {
      get { return isNull; }
    }

    MySqlDbType IMySqlValue.MySqlDbType
    {
      get { return MySqlDbType.UInt16; }
    }

    object IMySqlValue.Value
    {
      get { return mValue; }
    }

    public ushort Value
    {
      get { return mValue; }
    }

    Type IMySqlValue.SystemType
    {
      get { return typeof(ushort); }
    }

    string IMySqlValue.MySqlTypeName
    {
      get { return "SMALLINT"; }
    }

    void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
    {
      int v = (val is UInt16) ? (UInt16)val : Convert.ToUInt16(val);
      if (binary)
        packet.WriteInteger((long)v, 2);
      else
        packet.WriteStringNoNull(v.ToString());
    }

    IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
    {
      if (nullVal)
        return new MySqlUInt16(true);

      if (length == -1)
        return new MySqlUInt16((ushort)packet.ReadInteger(2));
      else
        return new MySqlUInt16(UInt16.Parse(packet.ReadString(length)));
    }

    void IMySqlValue.SkipValue(MySqlPacket packet)
    {
      packet.Position += 2;
    }

    #endregion

    internal static void SetDSInfo(MySqlSchemaCollection sc)
    {
      // we use name indexing because this method will only be called
      // when GetSchema is called for the DataSourceInformation 
      // collection and then it wil be cached.
      MySqlSchemaRow row = sc.AddRow();
      row["TypeName"] = "SMALLINT";
      row["ProviderDbType"] = MySqlDbType.UInt16;
      row["ColumnSize"] = 0;
      row["CreateFormat"] = "SMALLINT UNSIGNED";
      row["CreateParameters"] = null;
      row["DataType"] = "System.UInt16";
      row["IsAutoincrementable"] = true;
      row["IsBestMatch"] = true;
      row["IsCaseSensitive"] = false;
      row["IsFixedLength"] = true;
      row["IsFixedPrecisionScale"] = true;
      row["IsLong"] = false;
      row["IsNullable"] = true;
      row["IsSearchable"] = true;
      row["IsSearchableWithLike"] = false;
      row["IsUnsigned"] = true;
      row["MaximumScale"] = 0;
      row["MinimumScale"] = 0;
      row["IsConcurrencyType"] = DBNull.Value;
      row["IsLiteralSupported"] = false;
      row["LiteralPrefix"] = null;
      row["LiteralSuffix"] = null;
      row["NativeDataType"] = null;
    }
  }
}
