using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public class ValidatorUtility
    {


        public static bool RegexTest(string value, string pattern, RegexOptions options = RegexOptions.None)
        {
            if (String.IsNullOrWhiteSpace(value)) return false;
            return Regex.IsMatch(value, pattern, options);
        }

        public static bool IsChinaMobile(string value)
        {
            return RegexTest(value, RegexLib.ChinaMobile);
        }
    }
}
