using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Utility
{
    public static class StringExtensions
    {


        public static Validator<T> IsEmpty<T>(this Validator<T> value, Expression<Func<T, string>> expression)
        {
            return value.IsEmpty(expression, e => !String.IsNullOrWhiteSpace(e));
        }

        public static Validator<T> Min<T>(this Validator<T> value, Expression<Func<T, string>> expression, int min)
        {
            return value.Range(expression, e => e.Length > min);
        }

        public static Validator<T> Max<T>(this Validator<T> value, Expression<Func<T, string>> expression, int max)
        {
            return value.Range(expression, e => e.Length < max);
        }

        public static Validator<T> Range<T>(this Validator<T> value, Expression<Func<T, string>> expression, int min, int max)
        {
            return value.Range(expression, e => e.Length > min && e.Length < max);
        }
    }
}
