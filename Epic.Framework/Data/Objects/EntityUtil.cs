using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Objects
{
    internal static class EntityUtil
    {
        internal static NotSupportedException NotSupported()
        {
            NotSupportedException e = new NotSupportedException();
            //TraceExceptionAsReturnValue(e);
            return e;
        }

        internal static NotSupportedException NotSupported(string error)
        {
            NotSupportedException e = new NotSupportedException(error);
            //TraceExceptionAsReturnValue(e);
            return e;
        }


    }
}
