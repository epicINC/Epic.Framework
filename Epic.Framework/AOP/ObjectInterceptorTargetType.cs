using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.AOP
{
    /// <summary>
    /// 对象类型
    /// </summary>
    [Flags]
    internal enum ObjectInterceptorTargetType
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        Ctor = 1 << 0,

        /// <summary>
        /// 属性
        /// </summary>
        Property = 1 << 1,

        /// <summary>
        /// 方法
        /// </summary>
        Method = 1 << 2,

        PM = Property | Method,

        All = Ctor | Property | Method,
    }
}
