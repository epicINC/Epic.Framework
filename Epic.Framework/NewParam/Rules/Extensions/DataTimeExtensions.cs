using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.NewParam
{
    public static class RuleForExpressionDataTimeExtensions
    {
        public static RuleForExpression<T, DateTime> Range<T>(this RuleForExpression<T, DateTime> value, DateTime min, DateTime max, string message = null) where T : new()
        {
            return value.Valid(e => e > min && e < max, WebParamState.ValidateFail, message);
        }
    }
}
