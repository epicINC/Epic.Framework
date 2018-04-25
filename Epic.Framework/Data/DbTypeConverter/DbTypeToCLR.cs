using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Epic.Data
{
    /// <summary>
    /// S
    /// 数据类型转换 
    /// 20110225
    /// 预期补充 SQLType To CLR, DbType To CLR
    /// </summary>
    public static class DbTypeConverter
    {


        static Dictionary<string, DbType> sqlToDbType;
        public static Dictionary<string, DbType> SqlToDbType
        {
            get
            {
                if (sqlToDbType == null)
                {
                    sqlToDbType = new Dictionary<string, DbType>();

                    #region 精确数字

                    sqlToDbType.Add("bigint", DbType.Int64);
                    sqlToDbType.Add("bit", DbType.Boolean);
                    sqlToDbType.Add("decimal ", DbType.Decimal);
                    sqlToDbType.Add("int", DbType.Int32);
                    sqlToDbType.Add("money", DbType.Currency);
                    sqlToDbType.Add("numeric", DbType.Decimal);
                    sqlToDbType.Add("smallint", DbType.Int16);
                    sqlToDbType.Add("smallmoney", DbType.Currency);
                    sqlToDbType.Add("tinyint", DbType.Byte);

                    #endregion

                    #region 近似数字

                    sqlToDbType.Add("float", DbType.Double);
                    sqlToDbType.Add("real", DbType.Single);

                    #endregion

                    #region 日期和时间

                    sqlToDbType.Add("date", DbType.Date);
                    sqlToDbType.Add("datetime2", DbType.DateTime2);
                    sqlToDbType.Add("datetime", DbType.DateTime);
                    sqlToDbType.Add("datetimeoffset", DbType.DateTimeOffset);
                    sqlToDbType.Add("smalldatetime", DbType.DateTime);
                    sqlToDbType.Add("time", DbType.Time);

                    #endregion

                    #region 字符串

                    sqlToDbType.Add("char", DbType.AnsiStringFixedLength);
                    sqlToDbType.Add("text", DbType.AnsiString);
                    sqlToDbType.Add("varchar", DbType.AnsiString);

                    #endregion

                    #region Unicode 字符串

                    sqlToDbType.Add("nchar", DbType.StringFixedLength);
                    sqlToDbType.Add("ntext", DbType.String);
                    sqlToDbType.Add("nvarchar", DbType.String);

                    #endregion

                    #region 二进制字符串

                    sqlToDbType.Add("binary", DbType.Binary);
                    sqlToDbType.Add("image", DbType.Binary);
                    sqlToDbType.Add("varbinary", DbType.Binary);

                    #endregion

                    #region 其他数据类型

                    //sqlToDbType.Add("cursor", DbType.AnsiString);
                    //sqlToDbType.Add("hierarchyid", DbType.AnsiString);
                    sqlToDbType.Add("sql_variant", DbType.Object);
                    sqlToDbType.Add("table", DbType.AnsiString);
                    sqlToDbType.Add("timestamp", DbType.Binary);
                    sqlToDbType.Add("uniqueidentifier", DbType.Guid);
                    sqlToDbType.Add("xml", DbType.Xml);

                    #endregion
                }
                return sqlToDbType;
            }
        }

        static Dictionary<Type, DbType> clrToDbType;
        public static Dictionary<Type, DbType> ClrToDbType
        {
            get
            {
                if (clrToDbType == null)
                {
                    clrToDbType = new Dictionary<Type, DbType>();
                    clrToDbType.Add(typeof(string), DbType.String);
                    clrToDbType.Add(typeof(byte[]), DbType.Binary);
                    clrToDbType.Add(typeof(bool), DbType.Boolean);
                    clrToDbType.Add(typeof(byte), DbType.Byte);
                    clrToDbType.Add(typeof(decimal), DbType.Decimal);
                    clrToDbType.Add(typeof(DateTime), DbType.DateTime);
                    clrToDbType.Add(typeof(double), DbType.Double);

                    clrToDbType.Add(typeof(short), DbType.Int16);
                    clrToDbType.Add(typeof(int), DbType.Int32);
                    clrToDbType.Add(typeof(long), DbType.Int64);
                    clrToDbType.Add(typeof(object), DbType.Object);
                    clrToDbType.Add(typeof(sbyte), DbType.SByte);
                    clrToDbType.Add(typeof(float), DbType.Single);

                    clrToDbType.Add(typeof(TimeSpan), DbType.Time);
                    clrToDbType.Add(typeof(ushort), DbType.UInt16);
                    clrToDbType.Add(typeof(uint), DbType.UInt32);
                    clrToDbType.Add(typeof(ulong), DbType.UInt64);
                    clrToDbType.Add(typeof(DateTimeOffset), DbType.DateTimeOffset);

                }
                return clrToDbType;
            }
        }


        public static DbType ToDbType(Type type)
        {
            DbType result;
            if (type.IsEnum)
                return ToDbType(type.GetEnumUnderlyingType());
            if (!ClrToDbType.TryGetValue(type, out result))
                throw Error.ArgumentNull(String.Format("给定值 {0} 不在字典中 {1}", type, "ClrToDbType"));

            return result;

        }

        public static DbType ToDbType(string sqlTypeName)
        {
            DbType result;
            if (!SqlToDbType.TryGetValue(sqlTypeName.ToLower(), out result))
                throw Error.ArgumentNull(String.Format("给定值 {0} 不在字典中 {1}", sqlTypeName, "SqlToDbType"));

            return result;

        }
    }
}
