using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Components
{
    /// <summary>
    /// 排序方向
    /// </summary>
    public enum SortDirection : byte
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,

        /// <summary>
        /// 倒序
        /// </summary>
        Desc = 1,

        /// <summary>
        /// 正序
        /// </summary>
        Asc = 2
    }
}
