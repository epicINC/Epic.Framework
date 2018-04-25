using System;

namespace Epic.Web
{
    public static class ByteParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<byte> Parse(this HttpParam<byte> param)
        {
            return param.Parse(Byte.TryParse);
        }

        #endregion

        #region 数字校验 Number Validator

        /// <summary>
        /// 大于 -1 的数字校验 (即 大于等于 0 的数字)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<byte> Positive(this HttpParam<byte> param)
        {
            return Min(param, 0);
        }

        /// <summary>
        /// 大于 0 的数字校验 (即 大于等于 1, 作为 ID 编号)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<byte> ID(this HttpParam<byte> param)
        {
            return Min(param, 1);
        }


        #endregion

        #region 范围 Range


        /// <summary>
        /// 最小长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="min">大于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<byte> Min(this HttpParam<byte> param, byte min)
        {
            param.Range(delegate(byte item)
            {
                if (item < min) return false;
                return true;
            });
            return param;
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="max">小于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<byte> Max(this HttpParam<byte> param, byte max)
        {
            param.Range(delegate(byte item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion
    }
}
