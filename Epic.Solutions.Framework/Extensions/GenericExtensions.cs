using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T value) where T : class
        {
            if (value == null)
                return Enumerable.Empty<T>();
            return new T[] { value };
        }
    }

    public static class GenericPublicExtensions
    {
        public static T Action<T>(this T value, Action<T> action)
        {
            action(value);
            return value;
        }

        public static K Func<T, K>(this T value, Func<T, K> func)
        {
            return func(value);
        }
    }
}
