using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit.Constructor.Extensions
{
    public static class ITypeChildConstructorExtensions
    {

        public static ITypeConstructor TypeConstructor(this ITypeChildConstructor value)
        {
            return EmitConstructor.Create(value.Context, value.Context.Type);
        }

        public static IPropertyConstructor DefinePropertyAutoImplemented<T>(this ITypeChildConstructor value, string name)
        {
            return value.TypeConstructor().DefinePropertyAutoImplemented<T>(name);
        }

        public static IPropertyConstructor DefinePropertyAutoImplemented(this ITypeChildConstructor value, string name, Type type)
        {
            return value.TypeConstructor().DefinePropertyAutoImplemented(name, type);
        }


        public static ITypeConstructor Create(this ITypeChildConstructor value, Action<Type> action = null)
        {
            return value.TypeConstructor().Create(action);
        }
    }
}
