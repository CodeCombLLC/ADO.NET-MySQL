// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See License.txt in the project root for license information.

using System;
using System.Data;
using Pomelo.Data.MySql;
using System.Globalization;

namespace Pomelo.Data.Types
{
  internal struct MySqlSingle : IMySqlValue
  {
    private float mValue;
    private bool isNull;

    public MySqlSingle(bool isNull)
    {
      this.isNull = isNull;
      mValue = 0.0f;
    }

    public MySqlSingle(float val)
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
      get { return MySqlDbType.Float; }
    }

    object IMySqlValue.Value
    {
      get { return mValue; }
    }

    public float Value
    {
      get { return mValue; }
    }

    Type IMySqlValue.SystemType
    {
      get { return typeof(float); }
    }

    string IMySqlValue.MySqlTypeName
    {
      get { return "FLOAT"; }
    }

    void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
    {
      Single v = (val is Single) ? (Single)val : Convert.ToSingle(val);
      if (binary)
        packet.Write(BitConverter.GetBytes(v));
      else
        packet.WriteStringNoNull(v.ToString("R",
   CultureInfo.InvariantCulture));
    }

    IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
    {
      if (nullVal)
        return new MySqlSingle(true);

      if (length == -1)
      {
        byte[] b = new byte[4];
        packet.Read(b, 0, 4);
        return new MySqlSingle(BitConverter.ToSingle(b, 0));
      }
      return new MySqlSingle(Single.Parse(packet.ReadString(length),
     CultureInfo.InvariantCulture));
    }

    void IMySqlValue.SkipValue(MySqlPacket packet)
    {
      packet.Position += 4;
    }

    #endregion

    internal static void SetDSInfo(MySqlSchemaCollection sc)
    {
      // we use name indexing because this method will only be called
      // when GetSchema is called for the DataSourceInformation 
      // collection and then it wil be cached.
      MySqlSchemaRow row = sc.AddRow();
      row["TypeName"] = "FLOAT";
      row["ProviderDbType"] = MySqlDbType.Float;
      row["ColumnSize"] = 0;
      row["CreateFormat"] = "FLOAT";
      row["CreateParameters"] = null;
      row["DataType"] = "System.Single";
      row["IsAutoincrementable"] = false;
      row["IsBestMatch"] = true;
      row["IsCaseSensitive"] = false;
      row["IsFixedLength"] = true;
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
      row["LiteralPrefix"] = null;
      row["LiteralSuffix"] = null;
      row["NativeDataType"] = null;
    }
  }
}