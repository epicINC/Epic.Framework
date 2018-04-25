using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    /// <summary>
    /// 内置 WebParam 缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    internal static class WebParamCache<T, K>
    {





        public static string Name
        {
            get;
            set;
        }

        public static Func<T, K> Get
        {
            get;
            set;
        }

        public static Action<T, K> Set
        {
            get;
            set;
        }

    }
}
