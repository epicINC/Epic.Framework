using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Extensions
{
    public static class IntExtensions
    {

        #region TryParse

        public static bool TryParseID(this string input, out int output)
        {
            output = 0;
            if (Int32.TryParse(input, out output))
                return output > 0;
            return false;
        }

        #endregion
    }
}
