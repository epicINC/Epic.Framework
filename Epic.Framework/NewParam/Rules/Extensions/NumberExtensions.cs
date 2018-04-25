using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    public static class RuleForExpressionNumberExtensions
    {
        public static RuleForExpression<T, byte> Range<T>(this RuleForExpression<T, byte> value, byte min, byte max, string message = null) where T : new()
        {
            return value.Valid(e => e > min && e < max, WebParamState.OutOfRange, message);     
        }

        public static RuleForExpression<T, int> Range<T>(this RuleForExpression<T, int> value, int min, int max, string message = null) where T : new()
        {
            return value.Valid(e => e > min && e < max, WebParamState.OutOfRange, message);
        }

        public static RuleForExpression<T, int> ID<T>(this RuleForExpression<T, int> value, string message = null) where T : new()
        {
            return value.Valid(e => e > 0, WebParamState.OutOfRange, message);
        }


        public static RuleForExpression<T, long> Range<T>(this RuleForExpression<T, long> value, long min, long max, string message = null) where T : new()
        {
            return value.Valid(e => e > 0, WebParamState.OutOfRange, message);
        }





    }
}
