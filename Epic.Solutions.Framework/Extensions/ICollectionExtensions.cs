using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static class ICollectionExtensions
    {




        public static ICollection<T> AddOrIgnore<T>(this ICollection<T> collection, T value, Action<T> action = null)
        {
            if (value != null)
            {
                if (action != null)
                    action(value);
                collection.Add(value);
            }
            return collection;
        }



        public static List<T> SelectIndex<T>(this ICollection collection, Func<int, T> func)
        {
            Errors.CheckArgumentNull(func, "func").Throw();

            var result = new List<T>();
            for (int i = 0; i < collection.Count; i++)
                result.Add(func(i));
            return result;
        }



        public static void ForIndex(this ICollection collection, Action<int> action)
        {
            Errors.CheckArgumentNull(action, "action").Throw();

            for (int i = 0; i < collection.Count; i++)
                action(i);
        }



        public static void ForEach(this IEnumerable collection, Action<object> action)
        {
            Errors.CheckArgumentNull(action, "action").Throw();

            foreach (var item in collection)
                action(item);
        }

        public static void ForEach<T>(this IEnumerable collection, Action<T> action)
        {
            Errors.CheckArgumentNull(action, "action").Throw();

            foreach (var item in collection)
                action((T)item);
        }
    }
}
