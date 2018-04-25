using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public static class IModuleConstructorExtension
    {

        public static ITypeConstructor DefineType(this IModuleConstructor value, string name, TypeAttributes attributes, Type parent = null, params Type[] interfaces)
        {
            return value.Create<TypeConstructor>(e => {
                e.Builder = value.Builder.DefineType(name, attributes, parent, interfaces);
                e.Context.Type = e;
            });
        }

    }
}