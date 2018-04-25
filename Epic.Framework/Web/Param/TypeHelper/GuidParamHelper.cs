using System;
using Epic.Extensions;

namespace Epic.Web
{
    public static class GuidParamHelper
    {
        #region Parse 转换方法

        public static HttpParam<Guid> Parse(this HttpParam<Guid> param)
        {
            return param.Parse(Guid.TryParse);
        }

        #endregion


    }
}
