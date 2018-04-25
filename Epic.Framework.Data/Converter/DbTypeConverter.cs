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
    public class DbTypeConverter
    {

        #region Init

        static Dictionary<Type, DbType> InitClrToDbType()
        {
            var result = new Dictionary<Type, DbType>();

            result.Add(typeof(string), DbType.String);
            result.Add(typeof(byte[]), DbType.Binary);
            result.Add(typeof(bool), DbType.Boolean);
            result.Add(typeof(byte), DbType.Byte);
            result.Add(typeof(decimal), DbType.Decimal);
            result.Add(typeof(DateTime), DbType.DateTime);
            result.Add(typeof(double), DbType.Double);

            result.Add(typeof(short), DbType.Int16);
            result.Add(typeof(int), DbType.Int32);
            result.Add(typeof(long), DbType.Int64);
            result.Add(typeof(object), DbType.Object);
            result.Add(typeof(sbyte), DbType.SByte);
            result.Add(typeof(float), DbType.Single);

            result.Add(typeof(TimeSpan), DbType.Time);
            result.Add(typeof(ushort), DbType.UInt16);
            result.Add(typeof(uint), DbType.UInt32);
            result.Add(typeof(ulong), DbType.UInt64);
            result.Add(typeof(DateTimeOffset), DbType.DateTimeOffset);

            return result;
        }

        #endregion

        static Dictionary<Type, DbType> ClrToDbType
        {
            get { return LazyLoad.Load(() => InitClrToDbType()); }
        }

        public static DbType AsDbType(Type value)
        {
            return ClrToDbType.Read(value).Func(e =>
                {
                    Errors.CheckArgumentNull(e, "key", () => "在集合中未找到 {0} 的项".Formating(value)).Throw();
                    return e;
                });
        }

    }
}
