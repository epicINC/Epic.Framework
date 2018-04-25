using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Framework
{
    #region custom delegate

    /// <summary>
    /// 类型转换委托
    /// </summary>
    /// <typeparam name="T">原数据类型</typeparam>
    /// <typeparam name="K">结果数据类型</typeparam>
    /// <param name="input">原数据</param>
    /// <param name="output">转换结果</param>
    /// <returns>转换是否成功</returns>
    public delegate bool ParseAction<T, K>(T input, out K output);

    #endregion
}

namespace Epic.Extensions
{
    /// <summary>
    /// Author:     SLIGHTBOY
    /// Description:	基础参数类型
    /// Copyright:		(c) 2000 - 2008 EpicLab Corporation, All rights reserved.
    /// </summary>
    /// <history>
    ///	Create:	SLIGHTBOY, 2008-4-11;
    public static class ConvertHelper
    {
        #region Parser






        #endregion






    }
}
