using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Utility
{
    public static class Condition
    {
        public static void When(bool condition)
        {

        }


        public static T Then<T>(this ConditionContainer value, Func<T> func)
        {
            return default(T);
        }
    }


    public class ConditionContainer
    {
        public bool Condition
        {
            get;
            internal set;
        }
    }
}
