using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Epic.Emit.RunSharp
{
    public class ClassGenerator : IMethodAttributes
    {

        


        public MethodAttributes Attributes
        {
            get;
            set;
        }

        public RunSharpContext Context
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
