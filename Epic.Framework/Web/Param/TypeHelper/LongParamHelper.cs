using System;

namespace Epic.Web
{
    public static class LongParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<long> Parse(this HttpParam<long> param)
        {
            return param.Parse(Int64.TryParse);
        }

        #endregion

        #region 数字校验 Number Validator

        /// <summary>
        /// Oicq 号码校验
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<long> Oicq(this HttpParam<long> param)
        {
            return param.Validator(Utility.ValidatorLibrary.OicqValidator);
        }


        /// <summary>
        /// 大于 -1 的数字校验 (即 大于等于 0 的数字)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<long> Positive(this HttpParam<long> param)
        {
            return Min(param, 0);
        }

        /// <summary>
        /// 大于 0 的数字校验 (即 大于等于 1, 作为 ID 编号)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<long> ID(this HttpParam<long> param)
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
        public static HttpParam<long> Min(this HttpParam<long> param, long min)
        {
            param.Range(delegate(long item)
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
        public static HttpParam<long> Max(this HttpParam<long> param, long max)
        {
            param.Range(delegate(long item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion


    }
}
