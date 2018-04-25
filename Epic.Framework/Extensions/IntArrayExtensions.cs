using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class IntArrayExtensions
    {
        #region TryParse

        public static bool TryParseArray(this string input, out int[] output)
        {
            return input.TryParseArray(',', out output);

        }
        public static bool TryParseArray(this string input, char separator, out int[] output)
        {
            return input.Split(separator).TryParse(out output);
        }

        public static bool TryParseArray(this string input, string separator, out int[] output)
        {
            return input.Split(separator.ToCharArray()).TryParse(out output);
        }

        #endregion
    }
}
