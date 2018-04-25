using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{

    public static class CommoUtility
    {
        public static T IIF<T>(bool condition, T left, T right)
        {
            return condition ? left : right;
        }

        public static void IIF(bool condition, Action left, Action right)
        {
            if (condition)
                left();
            else
                right();
        }

        public static T IIF<T>(bool condition, Func<T> left, Func<T> right)
        {
            return condition ? left() : right();
        }
    }
}
