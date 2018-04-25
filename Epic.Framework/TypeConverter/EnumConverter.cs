using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.TypeConverter
{
    public static class EnumConverter
    {

        /// <summary>
        /// 不约束 T 为值类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParseLess<T>(string value, out T result)
        {
            try
            {
                result = (T)Enum.Parse(typeof(T), value);
                return true;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }
    }
}
