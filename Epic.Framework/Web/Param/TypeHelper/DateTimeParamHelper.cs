using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Web
{
    public static class DateTimeParamHelper
    {
        #region 转换方法 Parse

        public static HttpParam<DateTime> Parse(this HttpParam<DateTime> param)
        {
            return param.Parse(DateTime.TryParse);
        }

        #endregion

        #region 范围 Range

        /// <summary>
        /// 最小长度
        /// </summary>
        /// <param name="param"></param>
        /// <param name="min">大于等于该值 为有效</param>
        /// <returns></returns>
        public static HttpParam<DateTime> Min(this HttpParam<DateTime> param, DateTime min)
        {
            param.Range(delegate(DateTime item)
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
        public static HttpParam<DateTime> Max(this HttpParam<DateTime> param, DateTime max)
        {
            param.Range(delegate(DateTime item)
            {
                if (item > max) return false;
                return true;
            });
            return param;
        }

        #endregion
    }
}
