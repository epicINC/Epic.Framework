using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public static class ICtorConstructorExtension
    {

        public static EmitGenerator GetILGenerator(this ICtorConstructor value)
        {
            return new SimpleEmitGenerator(value, value.Context.Ctor.GetILGenerator());
        }

        public static ITypeConstructor TypeConstructor(this ICtorConstructor value)
        {
            return EmitConstructor.Create(value.Context, value.Context.Type);
        }

    }
}
