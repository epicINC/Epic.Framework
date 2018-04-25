using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class ListExtensions
    {

        public static List<T> AddOrIgnore<T>(this List<T> collection, IEnumerable<T> value, Action<T> action = null)
        {
            if (value.IsNullOrEmpty()) return collection;
            return Add(collection, value, action);
        }

        public static List<T> Add<T>(this List<T> collection, IEnumerable<T> value, Action<T> action = null)
        {
            if (action != null)
                value.ForEach(action);
            collection.AddRange(value);
            return collection;
        }


    }
}
