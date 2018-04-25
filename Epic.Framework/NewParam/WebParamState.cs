using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    [Flags]
    public enum WebParamState
    {
        /// <summary>
        /// 默认 null
        /// </summary>
        Default = 1,

        /// <summary>
        /// 空 ""
        /// </summary>
        Empty = 2,

        /// <summary>
        /// 转换出错, 类型错误
        /// </summary>
        ParseError = 4,

        /// <summary>
        /// 超过范围
        /// </summary>
        OutOfRange = 8,

        /// <summary>
        /// 验证失败
        /// </summary>
        ValidateFail = 16,

        /// <summary>
        /// 有值
        /// </summary>
        HasValue = 32,

        /// <summary>
        /// 有效
        /// </summary>
        Vaild = Empty | HasValue 
    }
}
