using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Collections;

namespace Epic.Extensions
{

    public static partial class IEnumerableExtensions
    {
        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer)
        {
            return source.Intersect(target, new DynamicEqualityComparer<T>(comparer));
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer, out IEnumerable<T> except)
        {
            except = Except<T>(source, target, comparer);
            return Intersect<T>(source, target, comparer);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer)
        {
            return source.Except(target, new DynamicEqualityComparer<T>(comparer));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, IEnumerable<T> target, Func<T, T, bool> comparer, out IEnumerable<T> intersect)
        {
            intersect = Intersect<T>(source, target, comparer);
            return Except<T>(source, target, comparer);
        }


        public static IEnumerable<T> Intersect<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer)
        {
            var result = new List<T>();
            var i = 0;
            foreach (var item in source)
            {

                foreach (var local in target)
                {
                    if (comparer(item, local))
                    {
                        result.Add(item);
                        break;
                    } 
                }
                i++;
            }
            return result;
        }

        public static IEnumerable<T> Intersect<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer, out IEnumerable<T> except)
        {
            var result = Intersect<T, K>(source, target, comparer);
            except = source.Except(result);
            return result;
        }

        public static IEnumerable<T> Except<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer)
        {
            return source.Except(Intersect<T, K>(source, target, comparer));
        }

        public static IEnumerable<T> Except<T, K>(this IEnumerable<T> source, IEnumerable<K> target, Func<T, K, bool> comparer, out IEnumerable<T> intersect)
        {
            intersect = Intersect<T, K>(source, target, comparer);
            return source.Except(intersect);
        }

    }

}
