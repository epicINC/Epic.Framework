using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Extensions.FluentExtensions
{

    internal static class IListExtensionsTest
    {
        static void Test()
        {
            var list = new List<string>();


        }

    }

    public static class IListExtensions
    {

        public static IFluentRemoveContainer<T> Do<T, K>(this IFluentRemoveContainer<T> value, Action<T> action) where T : IList<K>
        {
            if (value.Value.IsNullOrEmpty()) return value;
            value.Do(action);
            return value;
        }

        public static IFluentRemoveContainer<T> First<T, K>(this IFluentRemoveContainer<T> value) where T : IList<K>
        {
            return value.Do<T, K>(e => e.RemoveAt(0));
        }

        public static IFluentRemoveContainer<T> Last<T, K>(this IFluentRemoveContainer<T> value) where T : IList<K>
        {
            return value.Do(e => e.RemoveAt(e.Count - 1));
        }
    }
}
