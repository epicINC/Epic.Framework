using System;

namespace Epic.Web
{
    public static class FloatParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<float> Parse(this HttpParam<float> param)
        {
            return param.Parse(Single.TryParse);
        }

        #endregion

        #region 范围 Range

        /// <summary>
        /// 最小长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="min">大于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<float> Min(this HttpParam<float> param, float min)
        {
            param.Range(delegate(float item)
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
        public static HttpParam<float> Max(this HttpParam<float> param, float max)
        {
            param.Range(delegate(float item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion
    }
}
