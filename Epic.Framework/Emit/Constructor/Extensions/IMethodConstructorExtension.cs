using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public static class IMethodConstructorExtension
    {
        public static EmitGenerator GetILGenerator(this IMethodConstructor value)
        {
            return new SimpleEmitGenerator(value, value.Context.Method.Builder.GetILGenerator());
        }
    }
}
