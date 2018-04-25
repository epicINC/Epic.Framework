using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.TypeConverter
{
    public static class ArrayConverter
    {
        public static bool TryParse<T, K>(ParseAction<T, K> parser, T[] value, out K[] result)
        {
            if (value == null || value.Length == 0)
            {
                result = new K[0];
                return false;
            }

            var array = new List<K>();

            K item;
            for (int i = 0; i < value.Length; i++)
            {
                if (parser(value[i], out item))
                    array.Add(item);
            }
            result = array.ToArray();
            return true;
        }
    }
}
