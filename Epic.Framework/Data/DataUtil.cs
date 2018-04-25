using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data
{
    internal class DataUtil
    {
        public static T CheckArgumentNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                
            }
            return value;
        }
    }
}
