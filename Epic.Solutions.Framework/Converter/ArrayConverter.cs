using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Converter
{
    public class ArrayConverter
    {


        #region Common Generic Array TryParse

        static bool InnerTryParse<T, K>(T value, out K result, TryParse<T, K> parser)
        {
            if (value != null)
                return parser(value, out result);

            result = default(K);
            return false;
        }

        static bool InnerTryParse<Input, Param, Output>(Input value, out Output result, Func<Input, Param> selector, TryParse<Param, Output> parser)
        {
            if (value != null)
                return InnerTryParse(selector(value), out result, parser);

            result = default(Output);
            return false;
        }

        public static Output[] Convert<Input, Output>(Input[] value, TryParse<Input, Output> parser, bool skip = false, Output defaultValue = default(Output))
        {
            if (value.Length == 0 || parser == null) return new Output[1];

            var result = new List<Output>();

            Output local;
            for (int i = 0; i < value.Length; i++)
            {
                if (!InnerTryParse<Input, Output>(value[i], out local, parser) && skip) continue;
                result.Add(local);
            }
            return result.ToArray();
        }

        public static Output[] Convert<Input, Output>(Input[] value, Func<Input, string> selector, TryParse<string, Output> parser, bool skip = false, Output defaultValue = default(Output))
        {
            return Convert<Input, string, Output>(value, selector, parser, skip, defaultValue);
        }

        public static Output[] Convert<Input, Param, Output>(Input[] value, Func<Input, Param> selector, TryParse<Param, Output> parser, bool skip = false, Output defaultValue = default(Output))
        {
            if (value.Length == 0 || selector == null || parser == null) return new Output[1];

            var result = new List<Output>();

            Output local;
            for (int i = 0; i < value.Length; i++)
            {
                if (!InnerTryParse<Input, Param, Output>(value[i], out local, selector, parser) && skip) continue;
                result.Add(local);
            }
            return result.ToArray();

        }

        #endregion


        #region Common Generic Array Converter

        static K InnerConvert<T, K>(T value, Converter<T, K> converter)
        {
            if (value != null)
                return converter(value);

            return default(K);
        }

        static Output InnerConvert<Input, Param, Output>(Input value, Func<Input, Param> selector, Converter<Param, Output> converter)
        {
            if (value != null)
                return InnerConvert(selector(value), converter);

            return default(Output);
        }

        public static Output[] Convert<Input, Output>(Input[] value, Converter<Input, Output> converter, bool skip = false, Output defaultValue = default(Output))
        {
            if (value.Length == 0 || converter == null) return new Output[1];

            var result = new List<Output>();

            for (int i = 0; i < value.Length; i++)
                result.Add(converter(value[i]));

            return result.ToArray();
        }

        public static Output[] Convert<Input, Output>(Input[] value, Func<Input, string> selector, Converter<string, Output> converter, bool skip = false, Output defaultValue = default(Output))
        {
            return Convert<Input, string, Output>(value, selector, converter, skip, defaultValue);
        }

        public static Output[] Convert<Input, Param, Output>(Input[] value, Func<Input, Param> selector, Converter<Param, Output> converter, bool skip = false, Output defaultValue = default(Output))
        {
            if (value.Length == 0 || selector == null || converter == null) return new Output[1];

            var result = new List<Output>();

            for (int i = 0; i < value.Length; i++)
                result.Add(converter(selector(value[i])));

            return result.ToArray();

        }

        #endregion

        
        public static string[] AsString<T>(T[] value, TryParse<T, string> parser, bool skip = false)
        {
            return AsString(value, parser, skip, String.Empty);
        }

        public static string[] AsString<T>(T[] value, TryParse<T, string> parser, bool skip, string defaultValue)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static string[] AsString<T>(T[] value, Func<T, string> selector, TryParse<string, string> parser, bool skip = false)
        {
            return AsString(value, selector, parser, skip, String.Empty);
        }

        public static string[] AsString<T>(T[] value, Func<T, string> selector, TryParse<string, string> parser, bool skip, string defaultValue)
        {
            return Convert(value, selector, parser, skip, defaultValue);
        }

        public static string[] AsString<T>(T[] value, Func<T, string> selector, Converter<string, string> converter, bool skip = false)
        {
            return AsString(value, selector, converter, skip, String.Empty);
        }

        public static string[] AsString<T>(T[] value, Func<T, string> selector, Converter<string, string> converter, bool skip, string defaultValue)
        {
            return Convert(value, selector, converter, skip, defaultValue);
        }


        public static bool[] AsBool<T>(T[] value, Func<T, string> selector, bool skip = false, bool defaultValue = false)
        {
            return Convert(value, selector, Boolean.TryParse, skip, defaultValue);
        }

        public static bool[] AsBool<T>(T[] value, TryParse<T, bool> parser, bool skip = false, bool defaultValue = false)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static bool[] AsBool<T>(T[] value, Converter<T, bool> converter, bool skip = false, bool defaultValue = false)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static byte[] AsByte<T>(T[] value, Func<T, string> selector, bool skip = false, byte defaultValue = 0)
        {
            return Convert(value, selector, Byte.TryParse, skip, defaultValue);
        }

        public static byte[] AsByte<T>(T[] value, TryParse<T, byte> parser, bool skip = false, byte defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static byte[] AsByte<T>(T[] value, Converter<T, byte> converter, bool skip = false, byte defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static ushort[] AsUInt16<T>(T[] value, Func<T, string> selector, bool skip = false, ushort defaultValue = 0)
        {
            return Convert(value, selector, UInt16.TryParse, skip, defaultValue);
        }

        public static ushort[] AsUInt16<T>(T[] value, TryParse<T, ushort> parser, bool skip = false, ushort defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static ushort[] AsUInt16<T>(T[] value, Converter<T, ushort> converter, bool skip = false, ushort defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static short[] AsInt16<T>(T[] value, Func<T, string> selector, bool skip = false, short defaultValue = 0)
        {
            return Convert(value, selector, Int16.TryParse, skip, defaultValue);
        }

        public static short[] AsInt16<T>(T[] value, TryParse<T, short> parser, bool skip = false, short defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static short[] AsInt16<T>(T[] value, Converter<T, short> converter, bool skip = false, short defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static uint[] AsUInt32<T>(T[] value, Func<T, string> selector, bool skip = false, uint defaultValue = 0)
        {
            return Convert(value, selector, UInt32.TryParse, skip, defaultValue);
        }

        public static uint[] AsUInt32<T>(T[] value, TryParse<T, uint> parser, bool skip = false, uint defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static uint[] AsUInt32<T>(T[] value, Converter<T, uint> converter, bool skip = false, uint defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static int[] AsInt32<T>(T[] value, Func<T, string> selector, bool skip = false, int defaultValue = 0)
        {
            return Convert(value, selector, Int32.TryParse, skip, defaultValue);
        }

        public static int[] AsInt32<T>(T[] value, TryParse<T, int> parser, bool skip = false, int defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static int[] AsInt32<T>(T[] value, Converter<T, int> converter, bool skip = false, int defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static ulong[] AsUInt64<T>(T[] value, Func<T, string> selector, bool skip = false, ulong defaultValue = 0)
        {
            return Convert(value, selector, UInt64.TryParse, skip, defaultValue);
        }

        public static ulong[] AsUInt64<T>(T[] value, TryParse<T, ulong> parser, bool skip = false, ulong defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static ulong[] AsUInt64<T>(T[] value, Converter<T, ulong> converter, bool skip = false, ulong defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }


        public static long[] AsInt64<T>(T[] value, Func<T, string> selector, bool skip = false, long defaultValue = 0)
        {
            return Convert(value, selector, Int64.TryParse, skip, defaultValue);
        }

        public static long[] AsInt64<T>(T[] value, TryParse<T, long> parser, bool skip = false, long defaultValue = 0)
        {
            return Convert(value, parser, skip, defaultValue);
        }

        public static long[] AsInt64<T>(T[] value, Converter<T, long> converter, bool skip = false, long defaultValue = 0)
        {
            return Convert(value, converter, skip, defaultValue);
        }
    }
}
