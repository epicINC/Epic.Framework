using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    /// <summary>
    /// 驗證器類型
    /// </summary>
    public enum WebParamValidatorNodeType
    {
        /// <summary>
        /// 必須
        /// </summary>
        Required,

        /// <summary>
        /// 轉換
        /// </summary>
        Parse,

        /// <summary>
        /// 轉換前
        /// </summary>
        PreParse,

        /// <summary>
        /// 轉換后
        /// </summary>
        EndParse




    }
}
