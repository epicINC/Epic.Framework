using Epic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epic.Extensions
{
    public static partial class StringExtensions
    {

        public static string Formatting(this string value, params object[] args)
        {
            return String.Format(value, args);
        }


        public static bool IsSame(this string value, string arg, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            if (value == null) return arg == null;
            return value.Equals(arg, comparisonType);
        }


        public static bool IsEmpty(this string value, Func<string, bool> action)
        {
            return value.Func(action);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return IsEmpty(value, String.IsNullOrEmpty);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return IsEmpty(value, String.IsNullOrWhiteSpace);
        }

    }
}
