using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{
    /// <summary>
    /// 表达式扩展函数
    /// </summary>
    public static class ExpressionExtensionMethods
    {
        /// <summary>
        /// 搜索 '%match%'
        /// </summary>
        /// <param name="value"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static bool Like(this string value, string match)
        {
            return true;
        }

        /// <summary>
        /// In
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In<T>(this T value, IEnumerable<T> array)
        {
            return true;
        }

        public static bool In<T>(this T value, params T[] array)
        {
            return true;
        }
    }
}
