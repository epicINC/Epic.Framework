using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.OpenAPI.Utility
{
    public class OAuthUtility
    {
        public static string RNG()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static double ElapsedSecond(DateTime value)
        {
            return Math.Floor(value.Subtract(DateTime.Now).TotalSeconds);
        }
    }
}
