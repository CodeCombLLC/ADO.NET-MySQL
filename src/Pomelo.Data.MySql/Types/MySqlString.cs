// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Data;
using System.IO;
using Pomelo.Data.MySql;

namespace Pomelo.Data.Types
{
  internal struct MySqlString : IMySqlValue
  {
    private string mValue;
    private bool isNull;
    private MySqlDbType type;

    public MySqlString(MySqlDbType type, bool isNull)
    {
      this.type = type;
      this.isNull = isNull;
      mValue = String.Empty;
    }

    public MySqlString(MySqlDbType type, string val)
    {
      this.type = type;
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
      get { return type; }
    }

    object IMySqlValue.Value
    {
      get { return mValue; }
    }

    public string Value
    {
      get { return mValue; }
    }

    Type IMySqlValue.SystemType
    {
      get { return typeof(string); }
    }

    string IMySqlValue.MySqlTypeName
    {
      get { return type == MySqlDbType.Set ? "SET" : type == MySqlDbType.Enum ? "ENUM" : "VARCHAR"; }
    }


    void IMySqlValue.WriteValue(MySqlPacket packet, bool binary, object val, int length)
    {
      string v = val.ToString();
      if (length > 0)
      {
        length = Math.Min(length, v.Length);
        v = v.Substring(0, length);
      }

      if (binary)
        packet.WriteLenString(v);
      else
        packet.WriteStringNoNull("'" + MySqlHelper.EscapeString(v) + "'");
    }

    IMySqlValue IMySqlValue.ReadValue(MySqlPacket packet, long length, bool nullVal)
    {
      if (nullVal)
        return new MySqlString(type, true);

      string s = String.Empty;
      if (length == -1)
        s = packet.ReadLenString();
      else
        s = packet.ReadString(length);
      MySqlString str = new MySqlString(type, s);
      return str;
    }

    void IMySqlValue.SkipValue(MySqlPacket packet)
    {
      int len = (int)packet.ReadFieldLength();
      packet.Position += len;
    }

    #endregion

    internal static void SetDSInfo(MySqlSchemaCollection sc)
    {
      string[] types = new string[] { "CHAR", "NCHAR", "VARCHAR", "NVARCHAR", "SET", 
                "ENUM", "TINYTEXT", "TEXT", "MEDIUMTEXT", "LONGTEXT" };
      MySqlDbType[] dbtype = new MySqlDbType[] { MySqlDbType.String, MySqlDbType.String,
                MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.Set, MySqlDbType.Enum, 
                MySqlDbType.TinyText, MySqlDbType.Text, MySqlDbType.MediumText, 
                MySqlDbType.LongText };

      // we use name indexing because this method will only be called
      // when GetSchema is called for the DataSourceInformation 
      // collection and then it wil be cached.
      for (int x = 0; x < types.Length; x++)
      {
        MySqlSchemaRow row = sc.AddRow();
        row["TypeName"] = types[x];
        row["ProviderDbType"] = dbtype[x];
        row["ColumnSize"] = 0;
        row["CreateFormat"] = x < 4 ? types[x] + "({0})" : types[x];
        row["CreateParameters"] = x < 4 ? "size" : null;
        row["DataType"] = "System.String";
        row["IsAutoincrementable"] = false;
        row["IsBestMatch"] = true;
        row["IsCaseSensitive"] = false;
        row["IsFixedLength"] = false;
        row["IsFixedPrecisionScale"] = true;
        row["IsLong"] = false;
        row["IsNullable"] = true;
        row["IsSearchable"] = true;
        row["IsSearchableWithLike"] = true;
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
}