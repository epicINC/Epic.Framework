using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Utility
{
    public static class StringUtil
    {
        public static string[] ObjectArrayToStringArray(object[] objectArray)
        {
            string[] array = new string[objectArray.Length];
            objectArray.CopyTo(array, 0);
            return array;
        }

    }
}
