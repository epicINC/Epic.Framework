using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Epic.Extensions
{
    public static class DateTimeExtensions
    {
        public static int WeekOfYear(this DateTime value)
        {
            return WeekOfYear(value, Thread.CurrentThread.CurrentCulture);
        }

        public static int WeekOfYear(this DateTime value, CultureInfo culture)
        {
            return culture.Calendar.GetWeekOfYear(value, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }
    
    }
}
