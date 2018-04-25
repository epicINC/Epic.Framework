using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    /// <summary>
    /// 引用自 System.Data.Entity.ModelConfiguration.Utilities
    /// </summary>
    public static class DynamicEqualityComparerLinqIntegration
    {
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> value, Func<T, T, bool> equalityComparer) where T : class
        {
            return value.Distinct(new DynamicEqualityComparer<T>(equalityComparer));
        }

        public static IEnumerable<IGrouping<T, T>> GroupBy<T>(this IEnumerable<T> value, Func<T, T, bool> equalityComparer) where T : class
        {
            return value.GroupBy((T t) => t, new DynamicEqualityComparer<T>(equalityComparer));
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> source, IEnumerable<T> dest, Func<T, T, bool> equalityComparer, Func<T, int> getHashCode = null)
        {
            if (source == null) Error.ArgumentNull("source");
            if (dest == null) Error.ArgumentNull("dest");
            if (equalityComparer == null) Error.ArgumentNull("equalityComparer");
            if (source.Count() == 0) return dest;
            if (dest.Count() == 0) return source;
            return source.Union(dest, new DynamicEqualityComparer<T>(equalityComparer, getHashCode));
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> source, IEnumerable<T> dest, Func<T, T, bool> equalityComparer, Func<T, int> getHashCode = null) where T : class
        {
            if (source == null) Error.ArgumentNull("source");
            if (dest == null) Error.ArgumentNull("dest");
            if (equalityComparer == null) Error.ArgumentNull("equalityComparer");
            if (source.Count() == 0) return dest;
            if (dest.Count() == 0) return source;
            return source.Intersect(dest, new DynamicEqualityComparer<T>(equalityComparer, getHashCode));
        }

        public static bool SequenceEqual<T>(this IEnumerable<T> source, IEnumerable<T> dest, Func<T, T, bool> equalityComparer) where T : class
        {
            if (source == null) return false;
            if (dest == null) return false;
            if (source.Count() == 0) return false;
            if (dest.Count() == 0) return false;
            if (equalityComparer == null) return false;
            return source.SequenceEqual(dest, new DynamicEqualityComparer<T>(equalityComparer));
        }

    }
}
