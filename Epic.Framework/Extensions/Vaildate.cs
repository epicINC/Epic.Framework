using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Epic.Extensions
{
    public static class VaildateHelper
    {
        static Regex email = new Regex("[\\w[.-]]+@[\\w[.-]]+\\.[\\w]+[\\.]?[\\w]+", RegexOptions.Compiled);

        /// <summary>
        /// 验证 string 为空返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!Char.IsWhiteSpace(value[i])) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 是否是Email 类型
        /// </summary>
        /// <param name="value">为正则表达式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsEmail(string value)
        {
            return email.IsMatch(value);
        }

        /// <summary>
        /// 是否是IsZipcode 类型
        /// </summary>
        /// <param name="value">为正则表达式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsZipcode(string value)
        {
            if (value.Length == 6)
            {
                int result;
                return Int32.TryParse(value, out result);
            }
            return false;
        }
        /// <summary>
        /// 必须为数字  
        /// 正则为^[0-9]{1,}$   _wyl
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsMustNum(string value) {
            return Regex.IsMatch(value, "^[0-9]{1,}$");
        }
    }
}
