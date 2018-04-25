using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public class TypeConstructor : BaseConstructor, ITypeConstructor
    {
        internal TypeAttributes attrs;

        public TypeBuilder Builder
        {
            get;
            set;
        }

    }


}
