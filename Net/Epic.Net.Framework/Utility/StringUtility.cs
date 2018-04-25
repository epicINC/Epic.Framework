using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Net.Utility
{
    public static class StringUtility
    {
        public static string MD5(string value)
        {
            return BitConverter.ToString(System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(value))).Replace("-", String.Empty);
        }
    }
}
