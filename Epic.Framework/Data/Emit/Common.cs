using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Emit
{
     internal static class Common
    {
         internal static Type Int32;

         static Common()
         {
             Int32 = typeof(System.Int32);

         }

    }
}
