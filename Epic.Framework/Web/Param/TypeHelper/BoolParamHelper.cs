using System;

namespace Epic.Web
{
    public static class BoolParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<bool> Parse(this HttpParam<bool> param)
        {
            return param.Parse(TryParse);
        }

        #endregion


        public static bool TryParse(string value, out bool result)
        {
            if (value == "1")
            {
                result = true;
                return true;
            }
            if (value == "0")
            {
                result = false;
                return true;
            }
            return Boolean.TryParse(value, out result);

        }

    }
}
