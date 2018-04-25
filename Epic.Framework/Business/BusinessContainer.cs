using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Business
{
    public static class BusinessContainer<T> where T : new()
    {
        static BusinessContainer()
        {
            container = new T();
        }

        static T container;
        public static T Current
        {
            get { return container; }
        }

        public static void Reset()
        {
            container = new T();
        }

    }
}
