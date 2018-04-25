using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class EnumExtensions
    {
        #region TryParse

        public static bool TryParseEnum<T>(this string input, out T output) where T : struct
        {
#if DEBUG
            if (!typeof(T).IsEnum) Error.ArgumentNull(typeof(T).Name + " 不是枚举类型!");
#endif
            try
            {
                output = (T)Enum.Parse(typeof(T), input, true);
                return true;
            }
            catch
            {
                output = default(T);
                return false;
            }
        }

        #endregion
    }
}
