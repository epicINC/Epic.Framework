using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Components;
using System.Globalization;

namespace Epic.Converter
{
    public static partial class StringConverter
    {
        public static string[] AsArray(string value)
        {
            return AsArray(value, ',');
        }

        public static string[] AsArray(string value, StringSplitOptions options)
        {
            return AsArray(value, new char[] { ',' }, options);
        }

        public static string[] AsArray(string value, params char[] separator)
        {
            if (String.IsNullOrEmpty(value)) return new string[] { };
            return value.Split(separator);
        }
        public static string[] AsArray(string value, char[] separator, StringSplitOptions options)
        {
            if (String.IsNullOrEmpty(value)) return new string[]{};
            return value.Split(separator, options);
        }

        public static string[] AsArray(string value, string[] separator, StringSplitOptions options)
        {
            if (String.IsNullOrEmpty(value)) return new string[] { };
            return value.Split(separator, options);
        }


        public static bool AsBool(string value, bool defaultValue = false)
        {
            if (String.IsNullOrWhiteSpace(value)) return defaultValue;
            value = value.Trim();
            if (value.Length == 1)
            {
                if (value[0] == '1') return true;
                if (value[0] == '0') return false;
            }
            if (Epic.Extensions.StringExtensions.IsSame(value, "true")) return true;
            if (Epic.Extensions.StringExtensions.IsSame(value, "false")) return false;

            return CommonConverter.AsBool(value, Boolean.TryParse, defaultValue);
        }

        public static byte AsByte(string value, byte defaultValue = 0)
        {
            return CommonConverter.AsByte(value, Byte.TryParse, defaultValue);
        }


        public static ushort AsUInt16(string value, ushort defaultValue = 0)
        {
            return CommonConverter.AsUInt16(value, UInt16.TryParse, defaultValue);
        }

        public static short AsInt16(string value, short defaultValue = 0)
        {
            return CommonConverter.AsInt16(value, Int16.TryParse, defaultValue);
        }


        public static uint AsUInt32(string value, uint defaultValue = 0)
        {
            return CommonConverter.AsUInt32(value, UInt32.TryParse, defaultValue);
        }

        public static int AsInt32(string value, int defaultValue = 0)
        {
            return CommonConverter.AsInt32(value, Int32.TryParse, defaultValue);
        }


        public static ulong AsUInt64(string value, ulong defaultValue = 0)
        {
            return CommonConverter.AsUInt64(value, UInt64.TryParse, defaultValue);
        }

        public static long AsInt64(string value, long defaultValue = 0)
        {
            return CommonConverter.AsInt64(value, Int64.TryParse, defaultValue);
        }


        public static double AsDouble(string value, double defaultValue = 0)
        {
            return CommonConverter.AsDouble(value, Double.TryParse, defaultValue);
        }

        public static decimal AsDecimal(string value, decimal defaultValue = 0)
        {
            return CommonConverter.AsDecimal(value, Decimal.TryParse, defaultValue);
        }

        public static decimal AsDecimal(string value,NumberStyles style, IFormatProvider provider, decimal defaultValue = 0)
        {
            return CommonConverter.AsDecimal(value, (string e, out decimal k) => Decimal.TryParse(e, style, provider, out k), defaultValue);
        }

        public static T AsEnum<T>(string value, T defaultValue = default(T)) where T : struct, IEnumConstraint
        {
            return CommonConverter.AsEnum(value, Enum.TryParse, defaultValue);
        }


        public static T AsEnum<T>(string value, bool ignoreCase, T defaultValue = default(T)) where T : struct, IEnumConstraint
        {
            return CommonConverter.AsEnum(value, (string e, out T result) => Enum.TryParse<T>(e, ignoreCase, out result), defaultValue);
        }


        public static DateTime AsDateTime(string value, DateTime defaultValue = default(DateTime))
        {
            return CommonConverter.AsDateTime(value, DateTime.TryParse, defaultValue);
        }

        public static DateTime AsDateTime(string value, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue = default(DateTime))
        {
            return CommonConverter.AsDateTime(value, (string e, out DateTime k) => DateTime.TryParse(e, provider, style, out k), defaultValue);
        }

        public static DateTime AsDateTimeExact(string value, string format, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue = default(DateTime))
        {
            return CommonConverter.AsDateTime(value, (string e, out DateTime k) => DateTime.TryParseExact(e, format, provider, style, out k), defaultValue);
        }

        public static DateTime AsDateTimeExact(string value, string[] formats, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue = default(DateTime))
        {
            return CommonConverter.AsDateTime(value, (string e, out DateTime k) => DateTime.TryParseExact(e, formats, provider, style, out k), defaultValue);
        }
    }
}
