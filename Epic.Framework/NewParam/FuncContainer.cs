using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{

    /// <summary>
    /// 参数对象 委托容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    internal class FuncContainer<T, K>
    {
        public FuncContainer(Func<T, K> func, string message = null)
        {
            this.Func = func;
            this.Message = message;
        }

        public Func<T, K> Func
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

    }
}
