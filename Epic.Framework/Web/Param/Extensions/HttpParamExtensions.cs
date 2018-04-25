using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Epic.Web
{
    public static class HttpParamExtensions
    {
        public static HttpParam<T> ToParam<T>(this NameValueCollection collection, string key)
        {
            return new HttpParam<T>(key, collection[key]);
        }

        public static bool IsValid<T>(this HttpParam<T> value)
        {
            return value != null && value.State == HttpParamStateType.Vaild;
        }

        /// <summary>
        /// 作为条件查询参数
        /// </summary>
        /// <returns></returns>
        public static HttpParam<T> QueryParam<T>(this HttpParam<T> value)
        {
            if (value != null)
                value.isParam = true;
            return value;
        }
    }
}
