using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{

    internal static class LazyLoadContainer<T>
    {

        static T current;

        public static T Current
        {
            get { return current; }
            internal set { current = value; }
        }

    }

    public class LazyLoad
    {
        public static T Load<T>() where T : new()
        {
            if (LazyLoadContainer<T>.Current != null)
                return LazyLoadContainer<T>.Current;
            return LazyLoadContainer<T>.Current = new T();
        }

        public static T Load<T>(Func<T> action)
        {
            if (LazyLoadContainer<T>.Current != null)
                return LazyLoadContainer<T>.Current;
            return LazyLoadContainer<T>.Current = action();
        }

    }
}
