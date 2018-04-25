using System;

namespace Epic.Web
{
    public static class DoubleParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<double> Parse(this HttpParam<double> param)
        {
            return param.Parse(Double.TryParse);
        }

        #endregion

        #region 范围 Range

        /// <summary>
        /// 最小长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="min">大于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<double> Min(this HttpParam<double> param, double min)
        {
            param.Range(delegate(double item)
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
        public static HttpParam<double> Max(this HttpParam<double> param, double max)
        {
            param.Range(delegate(double item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion
    }
}
