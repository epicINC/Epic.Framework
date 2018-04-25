using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public sealed class DynamicMethodBuilder : EmitGenerator
    {
        DynamicMethod method;

        public DynamicMethodBuilder(string name, Type returnType, params Type[] parameterTypes)
        {
            this.method = new DynamicMethod(name, returnType, parameterTypes);
            base.il = new ILGeneratorWrapper(this.method.GetILGenerator());
        }

        public T CreateDelegate<T>() where T : class
        {
            return this.method.CreateDelegate(typeof(T)) as T;
        }


    }

}
