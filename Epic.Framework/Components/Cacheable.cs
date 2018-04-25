using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Components
{

    public  class Cacheable<T>
        where T : class
    {
        static T current;
        static DateTime lastLoad;

        public static bool HasValue()
        {
            return current != null;
        }

        

        static int interval;
        /// <summary>
        /// 单位毫秒
        /// </summary>
        public static int Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        public static int IntervalSecond
        {
            get { return interval / 1000; }
            set { interval = 1000 * value; }
        }

        public static int IntervalMinute
        {
            get { return interval / (1000 * 60); }
            set { interval = 1000 * 60 * value; }
        }

        public static int IntervalHour
        {
            get { return interval / (1000 * 3600); }
            set { interval = 1000 * 3600 * value; }
        }


        public static T Current
        {
            get
            {
                CheckTimer();
                RaisLoad();
                return current;
            }
        }

        public static void SetValue(T k)
        {
            current = k;
        }

        public static event Func<T> OnLoad;

        static void RaisLoad()
        {
            if (current == null && OnLoad != null)
            { 
                current = OnLoad();
                UpdateTimer();
            }
        }

        static void CheckTimer()
        {

            if (interval > 0 && DateTime.Now.Subtract(lastLoad).TotalMilliseconds > interval)
                Clear();
        }

        static void UpdateTimer()
        {
            lastLoad = DateTime.Now;
        }

        public static void Clear()
        {
            current = null;
        }
    }

}
