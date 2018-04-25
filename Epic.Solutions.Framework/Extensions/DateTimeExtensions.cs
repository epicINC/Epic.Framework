using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Utility;

namespace Epic.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixStamp(this double value)
        {
            return DateTimeUtility.Unix.From(value);
        }

        public static double ToUnixStamp(this DateTime value)
        {
            return DateTimeUtility.Unix.To(value);
        }

        public static DateTime Tomorrow(this DateTime value)
        {
            return value.AddDays(1);
        }

        public static DateTime Yesterday(this DateTime value)
        {
            return value.AddDays(-1);
        }

        public static DateTime Weekend(this DateTime value, DayOfWeek day)
        {
            //return value.AddDays(value.DayOfWeek);
            return value;
        }
    }
}
