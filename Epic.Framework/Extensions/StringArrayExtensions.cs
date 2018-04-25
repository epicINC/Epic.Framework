using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Epic.Extensions
{
    public static class StringArrayExtensions
    {

        #region TryParse

        /// <summary>
        /// 转换 string[] to bool[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool  TryParse(this string[] value, out bool[] result, bool force = true)
        {
            return value.TryParse(out result, Boolean.TryParse, force);
        }

        /// <summary>
        /// 转换 string[] to byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool TryParse(this string[] value, out byte[] result, bool force = true)
        {
            return value.TryParse(out result, Byte.TryParse, force);
        }

        /// <summary>
        /// 转换 string[] to short[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool TryParse(this string[] value, out short[] result, bool force = true)
        {
            return value.TryParse(out result, Int16.TryParse, force);
        }

        /// <summary>
        /// 转换 string[] to int[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool TryParse(this string[] value, out int[] result, bool force = true)
        {
            return value.TryParse(out result, Int32.TryParse, force);
        }


        /// <summary>
        /// 转换 string[] to long[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool TryParse(this string[] value, out long[] result, bool force = true)
        {
            return value.TryParse(out result, Int64.TryParse, force);
        }


        /// <summary>
        /// 转换 string[] to double[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool TryParse(this string[] value, out double[] result, bool force = true)
        {
            return value.TryParse(out result, Double.TryParse, force);
        }

        /// <summary>
        /// 转换 string[] to decimal[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static bool TryParse(this string[] value, out decimal[] result, bool force = true)
        {
            return value.TryParse(out result, Decimal.TryParse, force);
        }

        #endregion

        public static bool TryParseEnum<T>(this string[] value, out T[] result, bool force = true)  where T : struct
        {
            return value.TryParse(out result, Enum.TryParse, force);
        }

    }
}
