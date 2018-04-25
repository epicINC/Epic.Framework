using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.TypeConverter;

namespace Epic.NewParam
{
    internal static class DefaultParseFunc<T>
    {

        static DefaultParseFunc()
        {
            InitTypeCodeConverter();
            InitArrayConverter();
        }

        static void InitTypeCodeConverter()
        {
            converter = new Dictionary<Type, object>();
            /*
            converter.Add(TypeCode.Boolean, (ParseAction<string, bool>)Boolean.TryParse);
            converter.Add(TypeCode.Byte, (ParseAction<string, byte>)Byte.TryParse);
            converter.Add(TypeCode.DateTime, (ParseAction<string, DateTime>)DateTime.TryParse);
            converter.Add(TypeCode.Decimal, (ParseAction<string, decimal>)Decimal.TryParse);
            converter.Add(TypeCode.Double, (ParseAction<string, double>)Double.TryParse);
            converter.Add(TypeCode.Int16, (ParseAction<string, short>)Int16.TryParse);
            converter.Add(TypeCode.Int32, (ParseAction<string, int>)Int32.TryParse);
            converter.Add(TypeCode.Int64, (ParseAction<string, long>)Int64.TryParse);
            converter.Add(TypeCode.SByte, (ParseAction<string, sbyte>)SByte.TryParse);
            converter.Add(TypeCode.Single, (ParseAction<string, Single>)Single.TryParse);
            converter.Add(TypeCode.UInt16, (ParseAction<string, ushort>)UInt16.TryParse);
            converter.Add(TypeCode.UInt32, (ParseAction<string, uint>)UInt32.TryParse);
            converter.Add(TypeCode.UInt64, (ParseAction<string, ulong>)UInt64.TryParse);
            */

            converter.Add(typeof(bool), (ParseAction<string, bool>)Boolean.TryParse);
            converter.Add(typeof(byte), (ParseAction<string, byte>)Byte.TryParse);
            converter.Add(typeof(short), (ParseAction<string, short>)Int16.TryParse);
            converter.Add(typeof(int), (ParseAction<string, int>)Int32.TryParse);
            converter.Add(typeof(long), (ParseAction<string, long>)Int64.TryParse);
            converter.Add(typeof(Single), (ParseAction<string, Single>)Single.TryParse);
            converter.Add(typeof(decimal), (ParseAction<string, decimal>)Decimal.TryParse);
            converter.Add(typeof(double), (ParseAction<string, double>)Double.TryParse);
            converter.Add(typeof(sbyte), (ParseAction<string, sbyte>)SByte.TryParse);
            converter.Add(typeof(ushort), (ParseAction<string, ushort>)UInt16.TryParse);
            converter.Add(typeof(uint), (ParseAction<string, uint>)UInt32.TryParse);
            converter.Add(typeof(ulong), (ParseAction<string, ulong>)UInt64.TryParse);
            converter.Add(typeof(DateTime), (ParseAction<string, DateTime>)DateTime.TryParse);

            converter.Add(typeof(string), (ParseAction<string, string>)ParseString);
        }

        static void InitArrayConverter()
        {
            arrayConverter = new Dictionary<Type, object>();
            arrayConverter.Add(typeof(bool), (ParseAction<string, bool[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(byte), (ParseAction<string, byte[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(short), (ParseAction<string, short[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(int), (ParseAction<string, int[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(long), (ParseAction<string, long[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(Single), (ParseAction<string, Single[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(double), (ParseAction<string, double[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(decimal), (ParseAction<string, decimal[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(sbyte), (ParseAction<string, sbyte[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(ushort), (ParseAction<string, ushort[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(uint), (ParseAction<string, uint[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(ulong), (ParseAction<string, ulong[]>)StringConverter.TryParse);
            arrayConverter.Add(typeof(DateTime), (ParseAction<string, DateTime[]>)StringConverter.TryParse);
        }

        static Dictionary<Type, object> converter;
        static Dictionary<Type, object> arrayConverter;

        #region Parser


        static bool ParseString(string value, out string result)
        {

            if (!String.IsNullOrWhiteSpace(value))
            {
                result = value.Trim();
                return true;
            }
            result = null;
            return false;
        }


        static ParseAction<string, K[]> EnumLessArrayParser<K>()
        {
            return StringConverter.TryParseEnumLess<K>;
        }


        #endregion


        public static ParseAction<string, K> Query<K>()
        {
            object result;
            converter.TryGetValue(typeof(K), out result);
            if (result == null)
            {
                /// 枚举判断
                if (typeof(K).IsEnum)
                {
                    return (ParseAction<string, K>)TypeConverter.EnumConverter.TryParseLess<K>;
                }

            }

            return (ParseAction<string, K>)result;
        }

        public static ParseAction<string, K[]> QueryArray<K>()
        {
            var type = typeof(K);

            object result;
            arrayConverter.TryGetValue(type, out result);
            if (result == null)
            {
                if (type.IsEnum)
                    return StringConverter.TryParseEnumLess<K>;
            }

            return (ParseAction<string, K[]>)result;
        }

  

    }
}
