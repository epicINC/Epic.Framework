using System;

namespace Epic.Web
{
    public static class CharParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<char> Parse(this HttpParam<char> param)
        {
            return param.Parse(Char.TryParse);
        }

        #endregion
    }
}
