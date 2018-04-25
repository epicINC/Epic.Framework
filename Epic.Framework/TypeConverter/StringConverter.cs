using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.TypeConverter
{
    /// <summary>
    /// string 转换工具
    /// </summary>
    public static class StringConverter
    {
        #region string To string[]

        public static bool TryParse(string value, out string[] result)
        {
            return TryParse(value, ',', out result);
        }

        public static bool TryParse(string value, char separator, out string[] result)
        {
            return TryParse(value, out result, separator);
        }

        public static bool TryParse(string value, out string[] result, params char[] separator)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                result = null;
                return false;
            }

            result = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return true;
        }

        #endregion

        #region string To array

        static string[] Convert(string value)
        {
            string[] result;
            TryParse(value, out result);
            return result;
        }

        public static bool TryParse(string value, out bool[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false;  }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out byte[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out short[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out int[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }


        public static bool TryParse(string value, out long[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out Single[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out double[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out decimal[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out sbyte[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out ushort[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out uint[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }
         
        public static bool TryParse(string value, out ulong[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParse(string value, out DateTime[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParse(item, out result);
        }

        public static bool TryParseEnum(Type arrayType, string value, out object result)
        {
            result = null;
            return false;
            //Enum.Parse()
        }

        public static bool TryParseEnum<T>(string value, out T[] result) where T : struct
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParseEnum(item, out result);
        }

        public static bool TryParseEnumLess<T>(string value, out T[] result)
        {
            var item = Convert(value);
            if (item == null) { result = null; return false; }
            return StringArrayConverter.TryParseEnumLess(item, out result);
        }

        #endregion


    }
}
