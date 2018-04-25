using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Epic.Utility
{
    public static class IntExtensions
    {
        public static Validator<T> ID<T>(this Validator<T> value, Expression<Func<T, int>> expression)
        {
            return value.Range(expression, e => e > 0);
        }

        public static Validator<T> Min<T>(this Validator<T> value, Expression<Func<T, int>> expression, int min)
        {
            return value.Range(expression, e => e > min);
        }

        public static Validator<T> Max<T>(this Validator<T> value, Expression<Func<T, int>> expression, int max)
        {
            return value.Range(expression, e => e < max);
        }

        public static Validator<T> Range<T>(this Validator<T> value, Expression<Func<T, int>> expression, int min, int max)
        {
            return value.Range(expression, e => e > min && e < max);
        }
    }
}
