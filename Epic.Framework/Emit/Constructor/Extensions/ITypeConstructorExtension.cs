using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Emit
{
    public static class ITypeConstructorExtension
    {

        public static ITypeConstructor GetTypeConstructor(this IFieldConstructor value)
        {
            return value.Context.Type;
        }

        public static IAssemblyConstructor GetAssemblyConstructor(this IFieldConstructor value)
        {
            return value.Context.Assembly;
        }


        public static IFieldConstructor DefineField(this ITypeConstructor value, string name, Type type, FieldAttributes attributes)
        {
            return value.Create<FieldConstructor>(e =>
            {
                e.Builder = value.Builder.DefineField(name, type, attributes);
                e.Context.Field = e;
            });
        }

        public static ICtorConstructor DefineConstructor(this ITypeConstructor value, MethodAttributes attributes, CallingConventions callingConvention)
        {
            return DefineConstructor(value, attributes, callingConvention, Type.EmptyTypes);
        }

        public static ICtorConstructor DefineConstructor(this ITypeConstructor value, MethodAttributes attributes, CallingConventions callingConvention, params Type[] parameterTypes)
        {
            return value.Create<CtorConstructor>(e =>
                {
                    e.Builder = value.Builder.DefineConstructor(attributes, callingConvention, parameterTypes);
                    e.Context.Ctor = e;
                });
        }



        #region Method

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, MethodInfo method)
        {
            return DefineMethod
                (
                    value,
                    method.Name, 
                    method.Attributes, 
                    method.CallingConvention,
                    method.ReturnType,
                    method.ReturnParameter.GetRequiredCustomModifiers(),
                    method.ReturnParameter.GetOptionalCustomModifiers(),
                    method.GetParameters().Select(e => e.ParameterType).ToArray(),
                    method.GetParameters().Select(e => e.GetRequiredCustomModifiers()).ToArray(),
                    method.GetParameters().Select(e => e.GetOptionalCustomModifiers()).ToArray()
                );
        }

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        {
            return value.Create<MethodConstructor>(e =>
            {
                e.Builder = value.Builder.DefineMethod
                    (
                        name,
                        attributes,
                        callingConvention,
                        returnType,
                        returnTypeRequiredCustomModifiers,
                        returnTypeOptionalCustomModifiers,
                        parameterTypes,
                        parameterTypeRequiredCustomModifiers,
                        parameterTypeOptionalCustomModifiers

                    );
                e.Context.Method = e;
            });
        }




        #endregion

        #region

        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, PropertyInfo property)
        {
            return value.DefineProperty
                (
                    property.Name,
                    property.Attributes,
                    CallingConventions.Standard,
                    property.PropertyType,
                    property.GetRequiredCustomModifiers(),
                    property.GetOptionalCustomModifiers(),
                    property.GetIndexParameters().Select(e => e.ParameterType).ToArray(),
                    property.GetIndexParameters().Select(e => e.GetRequiredCustomModifiers()).ToArray(),
                    property.GetIndexParameters().Select(e => e.GetOptionalCustomModifiers()).ToArray()
                );
        }

        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        {
            return value.Create<PropertyConstructor>(e =>
                {
                    e.Builder = value.Builder.DefineProperty
                        (
                            name,
                            attributes,
                            callingConvention,
                            returnType,
                            returnTypeRequiredCustomModifiers,
                            returnTypeOptionalCustomModifiers,
                            parameterTypes,
                            parameterTypeRequiredCustomModifiers,
                            parameterTypeOptionalCustomModifiers
                        );
                    e.Context.Property = e;
                });
        }
        #endregion


    }
}
