using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Epic.NewParam.DefaultParser
{
    public static class DateTimeParserExtensions
    {
        public static RuleForExpression<T, DateTime> Parse<T>(this RuleForExpression<T, DateTime> value, string message = null) where T : new()
        {
            return value.Parse<T, DateTime>(DateTime.TryParse, message);
        }

        public static RuleForExpression<T, DateTime> Parse<T>(this RuleForExpression<T, DateTime> value, IFormatProvider provider, DateTimeStyles styles, string message = null) where T : new()
        {
            return value.Parse<T, DateTime>((string s, out DateTime result) =>
            {
                return DateTime.TryParse(s, provider, styles, out result);
            }, message);
        }


        public static RuleForExpression<T, DateTime> Parse<T>(this RuleForExpression<T, DateTime> value, string format, IFormatProvider provider, DateTimeStyles styles, string message = null) where T : new()
        {
            return value.Parse<T, DateTime>((string s, out DateTime result) =>
            {
                return DateTime.TryParseExact(s, format, provider, styles, out result);
            }, message);
        }

        public static RuleForExpression<T, DateTime> Parse<T>(this RuleForExpression<T, DateTime> value, string[] formats, IFormatProvider provider, DateTimeStyles styles, string message = null) where T : new()
        {
            return value.Parse<T, DateTime>((string s, out DateTime result) =>
            {
                return DateTime.TryParseExact(s, formats, provider, styles, out result);
            }, message);
        }

    }
}
