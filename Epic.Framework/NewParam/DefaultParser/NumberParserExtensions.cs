using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    public static class NumberParserExtensions
    {

        public static RuleForExpression<T, byte> Parse<T>(this RuleForExpression<T, byte> value, string message = null) where T : new()
        {
            return value.Parse<T, byte>(Byte.TryParse, message);
        }

        public static RuleForExpression<T, short> Parse<T>(this RuleForExpression<T, short> value, string message = null) where T : new()
        {
            return value.Parse<T, short>(Int16.TryParse, message);
        }

        /// <summary>
        /// 转换成 Int, 使用 Int32.TryParse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="?"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RuleForExpression<T, int> Parse<T>(this RuleForExpression<T, int> value, string message = null) where T : new()
        {
            return value.Parse<T, int>(Int32.TryParse, message);
        }

        public static RuleForExpression<T, long> Parse<T>(this RuleForExpression<T, long> value, string message = null) where T : new()
        {
            return value.Parse<T, long>(Int64.TryParse, message);
        }


        public static RuleForExpression<T, ushort> Parse<T>(this RuleForExpression<T, ushort> value, string message = null) where T : new()
        {
            return value.Parse<T, ushort>(UInt16.TryParse, message);
        }


        public static RuleForExpression<T, uint> Parse<T>(this RuleForExpression<T, uint> value, string message = null) where T : new()
        {
            return value.Parse<T, uint>(UInt32.TryParse, message);
        }

        public static RuleForExpression<T, ulong> Parse<T>(this RuleForExpression<T, ulong> value, string message = null) where T : new()
        {
            return value.Parse<T, ulong>(UInt64.TryParse, message);
        }

    }


}
