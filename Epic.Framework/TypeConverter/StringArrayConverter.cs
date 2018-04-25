using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.TypeConverter
{
    public static class StringArrayConverter
    {
        public static bool TryParse(string[] value, out bool[] result)
        {
            return ArrayConverter.TryParse<string, bool>(Boolean.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out byte[] result)
        {
            return ArrayConverter.TryParse<string, byte>(Byte.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out short[] result)
        {
            return ArrayConverter.TryParse<string, short>(Int16.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out int[] result)
        {
            return ArrayConverter.TryParse<string, int>(Int32.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out long[] result)
        {
            return ArrayConverter.TryParse<string, long>(Int64.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out Single[] result)
        {
            return ArrayConverter.TryParse<string, Single>(Single.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out double[] result)
        {
            return ArrayConverter.TryParse<string, double>(Double.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out decimal[] result)
        {
            return ArrayConverter.TryParse<string, decimal>(Decimal.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out sbyte[] result)
        {
            return ArrayConverter.TryParse<string, sbyte>(SByte.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out ushort[] result)
        {
            return ArrayConverter.TryParse<string, ushort>(UInt16.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out uint[] result)
        {
            return ArrayConverter.TryParse<string, uint>(UInt32.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out ulong[] result)
        {
            return ArrayConverter.TryParse<string, ulong>(UInt64.TryParse, value, out result);
        }

        public static bool TryParse(string[] value, out DateTime[] result)
        {
            return ArrayConverter.TryParse<string, DateTime>(DateTime.TryParse, value, out result);
        }

        public static bool TryParseEnum<T>(string[] value, out T[] result) where T : struct
        {
            return ArrayConverter.TryParse<string, T>(Enum.TryParse, value, out result);
        }

        public static bool TryParseEnumLess<T>(string[] value, out T[] result)
        {
            return ArrayConverter.TryParse<string, T>(EnumConverter.TryParseLess<T>, value, out result);
        }
    }
}
