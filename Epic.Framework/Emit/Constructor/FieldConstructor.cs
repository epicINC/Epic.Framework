using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace Epic.Emit
{



    public class FieldConstructor : BaseConstructor, IFieldConstructor
    {
        public FieldBuilder Builder
        {
            get;
            set;
        }


    }
}
