using Epic.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Converter
{

    public static class CommonConverter
    {

        #region Common Generic TryParse

        public static Output Convert<Input, Output>(Input value, Func<Input, string> selector, TryParse<string, Output> parser, Output defaultValue = default(Output))
        {
            return Convert<Input, string, Output>(value, selector, parser, defaultValue);
        }

        public static Output Convert<Input, Output>(Input value, TryParse<Input, Output> parser, Output defaultValue = default(Output))
        {
            if (value == null) return defaultValue;
            Output result;
            return parser(value, out result) ? result : defaultValue;
        }

        public static Output Convert<Input, Param, Output>(Input value, Func<Input, Param> selector, TryParse<Param, Output> parser, Output defaultValue = default(Output))
        {
            if (value == null) return defaultValue;
            Output result;
            return parser(selector(value), out result) ? result : defaultValue;
        }

        #endregion

        #region Common Generic Converter

        public static Output Convert<Input, Output>(this Input value, Func<Input, string> selector, Converter<string, Output> converter, Output defaultValue = default(Output))
        {
            if (value == null) return defaultValue;
            return converter(selector(value));
        }

        public static Output Convert<Input, Output>(this Input value, Converter<Input, Output> converter, Output defaultValue = default(Output))
        {
            if (value == null) return defaultValue;
            return converter(value);
        }

        public static Output Convert<Input, Param, Output>(this Input value, Func<Input, Param> selector, Converter<Param, Output> converter, Output defaultValue = default(Output))
        {
            if (value == null) return defaultValue;
            return converter(selector(value));
        }

        #endregion

        #region Generic TryParse Converter

        public static string AsString<T>(T value, TryParse<T, string> parser)
        {
            return AsString(value, parser, String.Empty);
        }

        public static string AsString<T>(T value, TryParse<T, string> parser, string defaultValue)
        {
            return Convert(value, parser, defaultValue);
        }

        public static string AsString<T>(T value, Func<T, string> selector, TryParse<string, string> parser)
        {
            return AsString(value, selector, parser, String.Empty);
        }

        public static string AsString<T>(T value, Func<T, string> selector, TryParse<string, string> parser, string defaultValue)
        {
            return Convert(value, selector, parser, defaultValue);
        }

        public static string AsString<T>(T value, Func<T, string> selector, Converter<string, string> converter)
        {
            return AsString(value, selector, converter, String.Empty);
        }

        public static string AsString<T>(T value, Func<T, string> selector, Converter<string, string> converter, string defaultValue)
        {
            return Convert(value, selector, converter, defaultValue);
        }

        public static bool AsBool<T>(T value, Func<T, string> selector, bool defaultValue = false)
        {
            return Convert(value, selector, Boolean.TryParse, defaultValue);
        }

        public static bool AsBool<T>(T value, TryParse<T, bool> parser, bool defaultValue = false)
        {
            return Convert(value, parser, defaultValue);
        }

        public static bool AsBool<T>(T value, Converter<T, bool> converter, bool defaultValue = false)
        {
            return Convert(value, converter, defaultValue);
        }


        public static byte AsByte<T>(T value, Func<T, string> selector, byte defaultValue = 0)
        {
            return Convert(value, selector, Byte.TryParse, defaultValue);
        }

        public static Byte AsByte<T>(T value, TryParse<T, byte> parser, byte defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static byte AsByte<T>(T value, Converter<T, byte> converter, byte defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static ushort AsUInt16<T>(T value, Func<T, string> selector, ushort defaultValue = 0)
        {
            return Convert(value, selector, UInt16.TryParse, defaultValue);
        }

        public static ushort AsUInt16<T>(T value, TryParse<T, ushort> parser, ushort defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static ushort AsUInt16<T>(T value, Converter<T, ushort> converter, ushort defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static short AsInt16<T>(T value, Func<T, string> selector, short defaultValue = 0)
        {
            return Convert(value, selector, Int16.TryParse, defaultValue);
        }

        public static short AsInt16<T>(T value, TryParse<T, short> parser, short defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static short AsInt16<T>(T value, Converter<T, short> converter, short defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static uint AsUInt32<T>(T value, Func<T, string> selector, uint defaultValue = 0)
        {
            return Convert(value, selector, UInt32.TryParse, defaultValue);
        }

        public static uint AsUInt32<T>(T value, TryParse<T, uint> parser, uint defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static uint AsUInt32<T>(T value, Converter<T, uint> converter, uint defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static int AsInt32<T>(T value, Func<T, string> selector, int defaultValue = 0)
        {
            return Convert(value, selector, Int32.TryParse, defaultValue);
        }

        public static int AsInt32<T>(T value, TryParse<T, int> parser, int defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static int AsInt32<T>(T value, Converter<T, int> converter, int defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static ulong AsUInt64<T>(T value, Func<T, string> selector, ulong defaultValue = 0)
        {
            return Convert(value, selector, UInt64.TryParse, defaultValue);
        }

        public static ulong AsUInt64<T>(T value, TryParse<T, ulong> parser, ulong defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static ulong AsUInt64<T>(T value, Converter<T, ulong> converter, ulong defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static long AsInt64<T>(T value, Func<T, string> selector, long defaultValue = 0)
        {
            return Convert(value, selector, Int64.TryParse, defaultValue);
        }

        public static long AsInt64<T>(T value, TryParse<T, long> parser, long defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static long AsInt64<T>(T value, Converter<T, long> converter, long defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }

        public static double AsDouble<T>(T value, Func<T, string> selector, double defaultValue = 0)
        {
            return Convert(value, selector, Double.TryParse, defaultValue);
        }

        public static double AsDouble<T>(T value, TryParse<T, double> parser, double defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static double AsDouble<T>(T value, Converter<T, double> converter, double defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }

        
        public static decimal AsDecimal<T>(T value, Func<T, string> selector, decimal defaultValue = 0)
        {
            return Convert(value, selector, Decimal.TryParse, defaultValue);
        }

        public static decimal AsDecimal<T>(T value, Func<T, string> selector, NumberStyles style, IFormatProvider provider, decimal defaultValue = 0)
        {
            return Convert(value, selector, (string e, out decimal k) => Decimal.TryParse(e, style, provider, out k), defaultValue);
        }

        public static decimal AsDecimal<T>(T value, TryParse<T, decimal> parser, decimal defaultValue = 0)
        {
            return Convert(value, parser, defaultValue);
        }

        public static decimal AsDecimal<T>(T value, Converter<T, decimal> converter, decimal defaultValue = 0)
        {
            return Convert(value, converter, defaultValue);
        }


        public static K AsEnum<T, K>(T value, Func<T, string> selector, K defaultValue = default(K)) where K : struct, IEnumConstraint
        {
            return Convert(value, selector, Enum.TryParse, defaultValue);
        }
        public static K AsEnum<T, K>(T value, bool ignoreCase, Func<T, string> selector, K defaultValue = default(K)) where K : struct, IEnumConstraint
        {
            return Convert(value, selector, (string e, out K k) => Enum.TryParse<K>(e, ignoreCase, out k), defaultValue);
        }

        public static K AsEnum<T, K>(T value, TryParse<T, K> parser, K defaultValue = default(K)) where K : struct, IEnumConstraint
        {
            return Convert(value, parser, defaultValue);
        }

        public static K AsEnum<T, K>(T value, Converter<T, K> converter, K defaultValue = default(K)) where K : struct, IEnumConstraint
        {
            return Convert(value, converter, defaultValue);
        }




        public static DateTime AsDateTime<T>(T value, Func<T, string> selector, DateTime defaultValue)
        {
            return Convert(value, selector, DateTime.TryParse, defaultValue);
        }

        public static DateTime AsDateTime<T>(T value, Func<T, string> selector, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue)
        {
            return Convert(value, selector, (string e, out DateTime k) => DateTime.TryParse(e, provider, style, out k), defaultValue);
        }

        public static DateTime AsDateTimeExact<T>(T value, Func<T, string> selector, DateTimeStyles style, IFormatProvider provider, string format, DateTime defaultValue)
        {
            return Convert(value, selector, (string e, out DateTime k) => DateTime.TryParseExact(e, format, provider, style, out k), defaultValue);
        }

        public static DateTime AsDateTimeExact<T>(T value, Func<T, string> selector, string[] formats, DateTimeStyles style, IFormatProvider provider, DateTime defaultValue)
        {
            return Convert(value, selector, (string e, out DateTime k) => DateTime.TryParseExact(e, formats, provider, style, out k), defaultValue);
        }

        public static DateTime AsDateTime<T>(T value, TryParse<T, DateTime> parser, DateTime defaultValue)
        {
            return Convert(value, parser, defaultValue);
        }

        public static DateTime AsDateTime<T>(T value, Converter<T, DateTime> converter, DateTime defaultValue)
        {
            return Convert(value, converter, defaultValue);
        }



        #endregion

    }
}
