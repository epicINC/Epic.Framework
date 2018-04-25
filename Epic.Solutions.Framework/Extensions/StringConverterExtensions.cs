using Epic.Components;
using Epic.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    /// <summary>
    /// Plan: Add Nullable
    /// </summary>
    public static class StringConverterExtensions
    {

        public static bool AsBool(this string value, bool defaultValue = false)
        {
            return StringConverter.AsBool(value, defaultValue);
        }

        public static byte AsByte(this string value, byte defaultValue = 0)
        {
            return StringConverter.AsByte(value, defaultValue);
        }

        public static ushort AsUInt16(this string value, ushort defaultValue = 0)
        {
            return StringConverter.AsUInt16(value, defaultValue);
        }

        public static short AsInt16(this string value, short defaultValue = 0)
        {
            return StringConverter.AsInt16(value, defaultValue);
        }

        public static uint AsUInt32(this string value, uint defaultValue = 0)
        {
            return StringConverter.AsUInt32(value, defaultValue);
        }

        public static int AsInt32(this string value, int defaultValue = 0)
        {
            return StringConverter.AsInt32(value, defaultValue);
        }

        public static ulong AsUInt64(this string value, ulong defaultValue = 0)
        {
            return StringConverter.AsUInt64(value, defaultValue);
        }

        public static long AsInt64(this string value, long defaultValue = 0)
        {
            return StringConverter.AsInt64(value, defaultValue);
        }


        public static double AsDouble(this string value, double defaultValue = 0)
        {
            return StringConverter.AsDouble(value, defaultValue);
        }

        public static decimal AsDecimal(this string value, decimal defaultValue = 0)
        {
            return StringConverter.AsDecimal(value, defaultValue);
        }

        public static decimal AsDecimal(this string value, NumberStyles style, IFormatProvider provider, decimal defaultValue = 0)
        {
            return StringConverter.AsDecimal(value, style, provider, defaultValue);
        }


        public static T AsEnum<T>(this string value, T defaultValue = default(T)) where T : struct, IEnumConstraint
        {
            return StringConverter.AsEnum(value, defaultValue);
        }


        public static T AsEnum<T>(this string value, bool ignoreCase, T defaultValue = default(T)) where T : struct, IEnumConstraint
        {
            return StringConverter.AsEnum(value, ignoreCase, defaultValue);
        }


        public static DateTime AsDataTime(this string value, DateTime defaultValue = default(DateTime))
        {
            return StringConverter.AsDateTime(value, defaultValue);
        }

        public static DateTime AsDataTime(this string value, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue = default(DateTime))
        {
            return StringConverter.AsDateTime(value, style, provider, defaultValue);
        }

        public static DateTime AsDataTimeExact(this string value, string format, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue = default(DateTime))
        {
            return StringConverter.AsDateTimeExact(value, format, style, provider, defaultValue);
        }

        public static DateTime AsDataTimeExact(this string value, string[] formats, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue = default(DateTime))
        {
            return StringConverter.AsDateTimeExact(value, formats, style, provider, defaultValue);
        }

    }
}
