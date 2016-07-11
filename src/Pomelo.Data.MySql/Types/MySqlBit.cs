// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Data;
using Pomelo.Data.MySql;

namespace Pomelo.Data.Types
{
  /// <summary>
  /// Summary description for MySqlUInt64.
  /// </summary>
  internal struct MySqlBit : IMySqlValue
  {
    private ulong mValue;
    private bool isNull;
    private bool readAsString;

    public MySqlBit(bool isnull)
    {
      mValue = 0;
      isNull = isnull;
      readAsString = false;
    }

    public bool ReadAsString
    {
      get { return readAsString; }
      set { readAsString = value; }
    }

    public bool IsNull
    {
      get { return isNull; }
    }

    MySqlDbType IMySqlValue.MySqlDbType
    {
      get { return MySqlDbType.Bit; }
    }

    object IMySqlValue.Value
    {
      get
      {
        return mValue;
      }
    }

    Type IMySqlValue.SystemType
    {
      get
      {
        return typeof(UInt64);
      }
    }

    string IMySqlValue.MySqlTypeName
    {
      get { return "BIT"; }
    }

    public void WriteValue(MySqlPacket packet, bool binary, object value, int length)
    {
      ulong v = (value is UInt64) ? (UInt64)value : Convert.ToUInt64(value);
      if (binary)
        packet.WriteInteger((long)v, 8);
      else
        packet.WriteStringNoNull(v.ToString());
    }

    public IMySqlValue ReadValue(MySqlPacket packet, long length, bool isNull)
    {
      this.isNull = isNull;
      if (isNull)
        return this;

      if (length == -1)
        length = packet.ReadFieldLength();

      if (ReadAsString)
        mValue = UInt64.Parse(packet.ReadString(length));
      else
        mValue = (UInt64)packet.ReadBitValue((int)length);
      return this;
    }

    public void SkipValue(MySqlPacket packet)
    {
      int len = (int)packet.ReadFieldLength();
      packet.Position += len;
    }

    internal static void SetDSInfo(MySqlSchemaCollection sc)
    {
      // we use name indexing because this method will only be called
      // when GetSchema is called for the DataSourceInformation 
      // collection and then it wil be cached.
      MySqlSchemaRow row = sc.AddRow();
      row["TypeName"] = "BIT";
      row["ProviderDbType"] = MySqlDbType.Bit;
      row["ColumnSize"] = 64;
      row["CreateFormat"] = "BIT";
      row["CreateParameters"] = DBNull.Value; ;
      row["DataType"] = typeof(UInt64).ToString();
      row["IsAutoincrementable"] = false;
      row["IsBestMatch"] = true;
      row["IsCaseSensitive"] = false;
      row["IsFixedLength"] = false;
      row["IsFixedPrecisionScale"] = true;
      row["IsLong"] = false;
      row["IsNullable"] = true;
      row["IsSearchable"] = true;
      row["IsSearchableWithLike"] = false;
      row["IsUnsigned"] = false;
      row["MaximumScale"] = 0;
      row["MinimumScale"] = 0;
      row["IsConcurrencyType"] = DBNull.Value;
      row["IsLiteralSupported"] = false;
      row["LiteralPrefix"] = DBNull.Value;
      row["LiteralSuffix"] = DBNull.Value;
      row["NativeDataType"] = DBNull.Value;
    }
  }
}
