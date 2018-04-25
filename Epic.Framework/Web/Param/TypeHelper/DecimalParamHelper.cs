using System;

namespace Epic.Web
{
    public static class DecimalParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<decimal> Parse(this HttpParam<decimal> param)
        {
            return param.Parse(Decimal.TryParse);
        }

        #endregion

        #region 范围 Range

        /// <summary>
        /// 最小长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="min">大于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<decimal> Min(this HttpParam<decimal> param, decimal min)
        {
            param.Range(delegate(decimal item)
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
        public static HttpParam<decimal> Max(this HttpParam<decimal> param, decimal max)
        {
            param.Range(delegate(decimal item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion
    }
}
