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


        public static Type Typeof<T>()
        {
            return TypePool<T>.Value;
        }
    }






    public static class TypePool<T>
    {
        static TypePool()
        {
            Value = typeof(T);
        }


        public static Type Value
        {
            get;
            private set;
        }

    }
}
