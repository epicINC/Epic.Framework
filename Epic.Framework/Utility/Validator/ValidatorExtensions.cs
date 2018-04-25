using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Utility
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// 验证是否合法
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True 验证成功, False 验证失败</returns>
        public static bool AsBool(this ValidResultType value)
        {
            return value == ValidResultType.Default;
        }

        /// <summary>
        /// 验证是否合法
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True 验证成功, False 验证失败</returns>
        public static bool AsBool(this Dictionary<string, ValidResultType> value)
        {
            if (value.Count == 0) return true;
            return value.All(e => e.Value == ValidResultType.Default);
        }
    }
}