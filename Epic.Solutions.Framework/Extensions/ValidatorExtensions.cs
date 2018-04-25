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
        public static bool RegexTest(this string value, string pattern, RegexOptions options = RegexOptions.None)
        {
            return ValidatorUtility.RegexTest(value, pattern, options);
        }

        public static bool IsChinaMobile(this string value)
        {
            return ValidatorUtility.IsChinaMobile(value);
        }
    }
}
