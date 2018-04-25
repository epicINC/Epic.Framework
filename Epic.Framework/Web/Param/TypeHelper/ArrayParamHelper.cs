using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Extensions;

namespace Epic.Web
{
    public static class ArrayParamHelper
    {
        #region Bool[] 转换方法 Parse

        public static HttpParam<bool[]> ParseIDS(this HttpParam<bool[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<bool[]> Parse(this HttpParam<bool[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out bool[] result)
                {
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region Byte[] 转换方法 Parse

        public static HttpParam<byte[]> ParseIDS(this HttpParam<byte[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<byte[]> Parse(this HttpParam<byte[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out byte[] result)
                {
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region Short[] 转换方法 Parse

        public static HttpParam<short[]> ParseIDS(this HttpParam<short[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<short[]> Parse(this HttpParam<short[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out short[] result)
                {
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region Int[] 转换方法 Parse

        public static HttpParam<int[]> ParseIDS(this HttpParam<int[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<int[]> Parse(this HttpParam<int[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out int[] result)
                {
                    if (String.IsNullOrWhiteSpace(item))
                    {
                        result = null;
                        return false;
                    }
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region string[] 转换方法 Parse



        public static HttpParam<string[]> Parse(this HttpParam<string[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out string[] result)
                {
                    result = item.Split(separator);
                    return true;
                }
                );
        }

        #endregion

        #region Long[] 转换方法 Parse

        public static HttpParam<long[]> ParseIDS(this HttpParam<long[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<long[]> Parse(this HttpParam<long[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out long[] result)
                {
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region double[] 转换方法 Parse

        public static HttpParam<double[]> ParseIDS(this HttpParam<double[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<double[]> Parse(this HttpParam<double[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out double[] result)
                {
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region Long[] 转换方法 Parse

        public static HttpParam<decimal[]> ParseIDS(this HttpParam<decimal[]> param, bool force = true)
        {
            return param.Parse(",", force);
        }

        public static HttpParam<decimal[]> Parse(this HttpParam<decimal[]> param, string separator, bool force = true)
        {
            return param.Parse(
                delegate(string item, out decimal[] result)
                {
                    return item.Split(separator).TryParse(out result, force);
                }
                );
        }

        #endregion

        #region Enum[] 转换方法 Parse

        public static HttpParam<T[]> ParseEnum<T>(this HttpParam<T[]> param, string separator) where T : struct
        {
            return param.Parse(
                delegate(string item, out T[] result)
                {
                    result = item.ToEnumArray<T>(separator);
                    return result.Length > 0;
                }
                );
        }

        #endregion
    }
}
