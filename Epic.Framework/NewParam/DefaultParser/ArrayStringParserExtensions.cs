using Epic.TypeConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam.DefaultParser
{
    public static class ArrayStringParserExtensions
    {
        public static RuleForExpression<T, byte[]> Parse<T>(this RuleForExpression<T, byte[]> value, string message = null) where T : new()
        {
            return value.Parse<T, byte>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, int[]> Parse<T>(this RuleForExpression<T, int[]> value, string message = null) where T : new()
        {
            return value.Parse<T, int>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, long[]> Parse<T>(this RuleForExpression<T, long[]> value, string message = null) where T : new()
        {
            return value.Parse<T, long>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, float[]> Parse<T>(this RuleForExpression<T, float[]> value, string message = null) where T : new()
        {
            return value.Parse<T, float>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, double[]> Parse<T>(this RuleForExpression<T, double[]> value, string message = null) where T : new()
        {
            return value.Parse<T, double>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, decimal[]> Parse<T>(this RuleForExpression<T, decimal[]> value, string message = null) where T : new()
        {
            return value.Parse<T, decimal>(StringConverter.TryParse, message);
        }



        public static RuleForExpression<T, sbyte[]> Parse<T>(this RuleForExpression<T, sbyte[]> value, string message = null) where T : new()
        {
            return value.Parse<T, sbyte>(StringConverter.TryParse, message);
        }



        public static RuleForExpression<T, ushort[]> Parse<T>(this RuleForExpression<T, ushort[]> value, string message = null) where T : new()
        {
            return value.Parse<T, ushort>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, uint[]> Parse<T>(this RuleForExpression<T, uint[]> value, string message = null) where T : new()
        {
            return value.Parse<T, uint>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, ulong[]> Parse<T>(this RuleForExpression<T, ulong[]> value, string message = null) where T : new()
        {
            return value.Parse<T, ulong>(StringConverter.TryParse, message);
        }

        public static RuleForExpression<T, K[]> Parse<T, K>(this RuleForExpression<T, K[]> value, string message = null) where T : new()
        {
            return null;
        }

    }
}
