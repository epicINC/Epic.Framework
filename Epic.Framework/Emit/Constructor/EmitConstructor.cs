using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Epic.Properties;

namespace Epic.Emit
{
    public class EmitConstructor : IConstructor
    {
        
        public EmitConstructor()
        {
            this.Context = new EmitContext();
        }

        public IEmitContext Context
        {
            get;
            set;
        }

    }
}
