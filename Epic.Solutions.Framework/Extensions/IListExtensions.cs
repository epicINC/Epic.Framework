using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class IListExtensions
    {


        public static void Replace<T>(this IList<T> collection, T source, T target)
        {
            var index = collection.IndexOf(source);
            if (index == -1) return;
            collection.RemoveAt(index);
            collection.Insert(index, target);
        }

        public static void Remove<T>(this IList<T> collection, Func<T, bool> action)
        {
            if (collection.Count == 0) return;

            var removes = new List<int>();

            for (var i = 0; i < collection.Count; i++)
            {
                if (action(collection[i]))
                    removes.Add(i);
            }

            foreach (var item in removes)
            {
                collection.RemoveAt(item);
            }
        }

        public static void RemoveFirst<T>(this IList<T> value)
        {
            if (value.Count == 0) return;
            value.RemoveAt(0);
        }

        public static void RemoveLast<T>(this IList<T> value)
        {
            if (value.Count == 0) return;
            value.RemoveAt(value.Count - 1);
        }


        public static void For<T>(this IList<T> value, Action<int, T> action)
        {
            For<T>(value, 0, value.Count, action);
        }

        public static void For<T>(this IList<T> value, int start, int end, Action<int, T> action)
        {
            if (end > value.Count) end = value.Count;

            for (int i = start; i < end; i++)
            {
                action(i, value[i]);
            }
        }
    }
}
