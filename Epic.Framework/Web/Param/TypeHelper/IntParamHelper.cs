using System;

namespace Epic.Web
{
    public static class IntParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<int> Parse(this HttpParam<int> param)
        {
            return param.Parse(Int32.TryParse);
        }

        #endregion

        #region 数字校验 Number Validator

        /// <summary>
        /// 大于 -1 的数字校验 (即 大于等于 0 的数字)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<int> Positive(this HttpParam<int> param)
        {
            return Min(param, 0);
        }

        /// <summary>
        /// 大于 0 的数字校验 (即 大于等于 1, 作为 ID 编号)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static HttpParam<int> ID(this HttpParam<int> param)
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
        public static HttpParam<int> Min(this HttpParam<int> param, int min)
        {
            param.Range(delegate(int item)
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
        public static HttpParam<int> Max(this HttpParam<int> param, int max)
        {
            param.Range(delegate(int item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion
    }
}
