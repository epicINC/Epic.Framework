using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public static class StringUtility
    {

        public static string Random(int length)
        {
            return Security.Utility.Salt(length);
        }
    }
}
