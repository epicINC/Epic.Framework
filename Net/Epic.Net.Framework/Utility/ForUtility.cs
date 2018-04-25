using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Net.Utility
{
    public static class ForUtility
    {

        public static void For<T>(T[] collection, Action<T, int> action, int skip = 0)
        {
            for (int i = skip; i < collection.Length; i++)
            {
                action(collection[i], i);
            }
        }


        public static void For<T>(T[] collection, Action<T[], int> action, int skip = 0, int step = 1)
        {
            for (int i = skip; i < collection.Length && (i + step) <= collection.Length; i += step)
            {
                action(collection, i);
            }
        }

        public static void Step<T>(T[] collection, Action<T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1]), skip, 2);
        }

        public static void Step<T>(T[] collection, Action<T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2]), skip, 3);
        }

        public static void Step<T>(T[] collection, Action<T, T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2], e[i + 3]), skip, 4);
        }

        public static void Step<T>(T[] collection, Action<T, T, T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2], e[i + 3], e[i + 4]), skip, 5);
        }

        public static void Step<T>(T[] collection, Action<T, T, T, T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2], e[i + 3], e[i + 4], e[i + 5]), skip, 6);
        }

        public static void Step<T>(T[] collection, Action<T, T, T, T, T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2], e[i + 3], e[i + 4], e[i + 5], e[i + 6]), skip, 7);
        }

        public static void Step<T>(T[] collection, Action<T, T, T, T, T, T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2], e[i + 3], e[i + 4], e[i + 5], e[i + 6], e[i + 7]), skip, 8);
        }

        public static void Step<T>(T[] collection, Action<T, T, T, T, T, T, T, T, T> action, int skip = 0)
        {
            For<T>(collection, (e, i) => action(e[i], e[i + 1], e[i + 2], e[i + 3], e[i + 4], e[i + 5], e[i + 6], e[i + 7], e[i + 8]), skip, 9);
        }
    }
}
