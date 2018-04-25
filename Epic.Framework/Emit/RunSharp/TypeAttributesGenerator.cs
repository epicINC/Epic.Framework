using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Emit.RunSharp
{
    public class TypeAttributesGenerator : ITypeAttributes
    {
        public RunSharpContext Context
        {
            get;
            set;
        }

        public TypeAttributes Attributes
        {
            get;
            set;
        }
    }


}
