using System;
using Epic.Extensions;

namespace Epic.Web
{
    public static class EnumParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<T> Parse<T>(this HttpParam<T> param) where T : struct
        {
#if DEBUG
            if (!typeof(T).IsEnum) Error.ArgumentNull(typeof(T).Name + " 不是枚举类型!");
#endif
            return param.Parse(Enum.TryParse);
        }

        #endregion

    }
}
