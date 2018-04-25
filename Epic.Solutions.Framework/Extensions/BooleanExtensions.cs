using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Utility;

namespace Epic.Extensions
{
    public static class BooleanExtensions
    {
        public static T IIF<T>(this bool value, T left, T right)
        {
            return CommoUtility.IIF(value, left, right);
        }

        public static void IIF(this bool value, Action left, Action right)
        {
            CommoUtility.IIF(value, left, right);
        }

        public static T IIF<T>(this bool value, Func<T> left, Func<T> right)
        {
            return CommoUtility.IIF(value, left, right);
        }
    } 
}
