using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class ArrayExtensions
    {

        public static bool TrueForAll<T>(this T[] value, Predicate<T> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            for (int i = 0; i < value.Length; i++)
            {
                if (match(value[i])) return false;
            }

            return true;
        }

    }




}
