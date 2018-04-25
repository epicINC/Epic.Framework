using Epic.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Converter
{
    public class SqlDbTypeConverter
    {
        #region Init

        static Dictionary<string, DbType> InitSqlToDbType()
        {
            var result = new Dictionary<string, DbType>();

            #region 精确数字

            result.Add("bigint", DbType.Int64);
            result.Add("bit", DbType.Boolean);
            result.Add("decimal ", DbType.Decimal);
            result.Add("int", DbType.Int32);
            result.Add("money", DbType.Currency);
            result.Add("numeric", DbType.Decimal);
            result.Add("smallint", DbType.Int16);
            result.Add("smallmoney", DbType.Currency);
            result.Add("tinyint", DbType.Byte);

            #endregion

            #region 近似数字

            result.Add("float", DbType.Double);
            result.Add("real", DbType.Single);

            #endregion

            #region 日期和时间

            result.Add("date", DbType.Date);
            result.Add("datetime2", DbType.DateTime2);
            result.Add("datetime", DbType.DateTime);
            result.Add("datetimeoffset", DbType.DateTimeOffset);
            result.Add("smalldatetime", DbType.DateTime);
            result.Add("time", DbType.Time);

            #endregion

            #region 字符串

            result.Add("char", DbType.AnsiStringFixedLength);
            result.Add("text", DbType.AnsiString);
            result.Add("varchar", DbType.AnsiString);

            #endregion

            #region Unicode 字符串

            result.Add("nchar", DbType.StringFixedLength);
            result.Add("ntext", DbType.String);
            result.Add("nvarchar", DbType.String);

            #endregion

            #region 二进制字符串

            result.Add("binary", DbType.Binary);
            result.Add("image", DbType.Binary);
            result.Add("varbinary", DbType.Binary);

            #endregion

            #region 其他数据类型

            //result.Add("cursor", DbType.AnsiString);
            //result.Add("hierarchyid", DbType.AnsiString);
            result.Add("sql_variant", DbType.Object);
            result.Add("table", DbType.AnsiString);
            result.Add("timestamp", DbType.Binary);
            result.Add("uniqueidentifier", DbType.Guid);
            result.Add("xml", DbType.Xml);

            #endregion

            return result;
        }

        #endregion


        static Dictionary<string, DbType> SqlToDbType
        {
            get { return LazyLoad.Load(() => InitSqlToDbType()); }
        }


        public static DbType AsDbType(string value)
        {
            return SqlToDbType.Read(value).Func(e =>
            {
                Errors.CheckArgumentNull(e, "key", () => "未在字段中找到 {0} 的项目".Formating(value)).Throw();
                return e;
            });
        }
    }
}
