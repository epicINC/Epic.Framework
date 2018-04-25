using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Emit
{
    public static class TypesPool
    {


        static TypesPool()
        {
            Void = typeof(void);

            Object = typeof(object);
            ObjectArray = typeof(object[]);
        }

        public static Type Void;
        public static Type Object;
        public static Type ObjectArray;
    }
}
