using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public class SimpleEmitGenerator : EmitGenerator
    {
        public SimpleEmitGenerator(ILGenerator il)
        {
            base.il = new ILGeneratorWrapper(il);
        }

        public SimpleEmitGenerator(IConstructor emitConstructor, ILGenerator il)
        {
            base.emitConstructor = emitConstructor;
            base.il = new ILGeneratorWrapper(il);
        }

    }
}
