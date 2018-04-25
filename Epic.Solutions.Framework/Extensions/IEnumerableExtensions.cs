using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static partial class IEnumerableExtensions
    {
        public static string StringJoin<T>(this IEnumerable<T> collection, string separator, Func<T, string> converter)
        {
            return String.Join(separator, collection.Select(converter));
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }


        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            Errors.CheckArgumentNull(action, "action").Throw();

            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static IEnumerable<T> TryCast<T>(this IEnumerable source) where T : class
        {
            T result;
            foreach (var item in source)
            {
                result = item as T;
                if (result == null)
                    continue;
                yield return result;
            }
        }

    }
}
