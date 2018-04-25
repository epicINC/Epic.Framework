using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic
{
    /// <summary>
    /// Phantom
    /// </summary>
    public static class ClassHelper
    {
        public static string LazyLoad(string value, Func<string> func)
        {
            if (String.IsNullOrEmpty(value))
                value = func();
            return value;
        }

        public static T LazyLoad<T>(T value) where T : new()
        {
            if (value == null)
                value = new T();
            return value;
        }

        public static T LazyLoad<T>(ref T value, Func<T> func)
        {
            if (value == null)
                value = func();
            return value;
        }


        
    }
}
