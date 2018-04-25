using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// 16进制字符串 转换成为 byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBitArray(this string value)
        {
            return Epic.Converter.StringConverter.ToBitArray(value);
        }
    }


    public static partial class ByteArrayExtensions
    {
        /// <summary>
        /// byte[] 转换成 16进制字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] value)
        {
            return Epic.Converter.ByteArrayConverter.ToHexString(value);
        }
    }
}
