// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using System;
using System.Data;
using System.IO;
using Pomelo.Data.MySql;

namespace Pomelo.Data.Types
{
    internal struct MySqlJson : IMySqlValue
    {
        private string mValue;
        private bool isNull;

        public MySqlJson(bool isNull)
        {
            this.isNull = isNull;
            mValue = String.Empty;
        }

        public MySqlJson(string val)
        {
            this.isNull = false;
            mValue = val;
        }

        #region IMySqlValue Members

        public bool IsNull
        {
            get { return isNull; }
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

        MySqlDbType IMySqlValue.MySqlDbType
        {
            get { return MySqlDbType.JSON; }
        }

        public string MySqlTypeName
        {
            get
            {
                return "JSON";
            }
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

        void IMySqlValue.SkipValue(MySqlPacket packet)
        {
            int len = (int)packet.ReadFieldLength();
            packet.Position += len;
        }

        #endregion

        internal static void SetDSInfo(MySqlSchemaCollection sc)
        {
            MySqlSchemaRow row = sc.AddRow();
            row["TypeName"] = "JSON";
            row["ProviderDbType"] = MySqlDbType.JSON;
            row["ColumnSize"] = 0;
            row["CreateFormat"] = "JSON";
            row["CreateParameters"] = null;
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

        public IMySqlValue ReadValue(MySqlPacket packet, long length, bool isNull)
        {
            if (isNull)
                return new MySqlJson(true);

            string s = String.Empty;
            if (length == -1)
                s = packet.ReadLenString();
            else
                s = packet.ReadString(length);
            MySqlJson str = new MySqlJson(s);
            return str;
        }
    }
}