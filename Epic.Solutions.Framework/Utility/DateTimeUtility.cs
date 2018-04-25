using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public static class DateTimeUtility
    {
        /// <summary>
        /// Unix 时间戳
        /// </summary>
        public static class Unix
        {
            public static DateTime Zero = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));


            public static DateTime From(double value)
            {
                return Zero.AddMilliseconds(value);
            }

            public static double To(DateTime value)
            {
                return Math.Floor((value - Zero).TotalMilliseconds);
            }
        }
    }
}

