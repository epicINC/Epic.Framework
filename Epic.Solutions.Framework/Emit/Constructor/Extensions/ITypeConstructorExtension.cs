using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Emit
{
    public static class ITypeConstructorExtension
    {

        public static IAssemblyConstructor AssemblyConstructor(this ITypeConstructor value)
        {
            return EmitConstructor.Create(value.Context, value.Context.Assembly);
        }



        public static IFieldConstructor DefineField(this ITypeConstructor value, string name, Type type, FieldAttributes attributes)
        {

            return EmitConstructor.Create(value.Context, value.Builder.DefineField(name, type, attributes));
        }



        public static IConstructorDefine DefineCtor(this ITypeConstructor value)
        {
            return EmitConstructor.Create<ConstructorDefine>(value);
        }

        public static ICtorConstructor DefineCtor(this ITypeConstructor value, MethodAttributes attributes, CallingConventions callingConvention)
        {
            return DefineCtor(value, attributes, callingConvention, Type.EmptyTypes);
        }

        public static ICtorConstructor DefineCtor(this ITypeConstructor value, MethodAttributes attributes, CallingConventions callingConvention, params Type[] parameterTypes)
        {
            return DefineCtor(value, attributes, callingConvention, parameterTypes, null, null);
        }

        public static ICtorConstructor DefineCtor(this ITypeConstructor value, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
        {
            return EmitConstructor.Create(value.Context, value.Builder.DefineConstructor(attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers));
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

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes)
        {
            return DefineMethod(value, name, attributes, CallingConventions.Standard, null, null);
        }

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes, CallingConventions callingConvention)
        {
            return DefineMethod(value, name, attributes, callingConvention, null, null);
        }

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes, Type returnType)
        {
            return DefineMethod(value, name, attributes, CallingConventions.Standard, returnType, null);
        }

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes, Type returnType, params Type[] parameterTypes)
        {
            return DefineMethod(value, name, attributes, CallingConventions.Standard, returnType, parameterTypes);
        }

        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
        {
            return DefineMethod(value, name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
        }


        public static IMethodConstructor DefineMethod(this ITypeConstructor value, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        {
            return EmitConstructor.Create(value.Context, value.Builder.DefineMethod
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

                ));
        }




        #endregion

        #region DefineProperty


        public static IPropertyConstructor DefinePropertyAutoImplemented<T>(this ITypeConstructor value, string name)
        {
            return DefinePropertyAutoImplemented(value, name, TypesPool.Typeof<T>());
        }


        public static IPropertyConstructor DefinePropertyAutoImplemented(this ITypeConstructor value, string name, Type type)
        {
            var field = value.DefineField("<{0}>k__BackingField".Formatting(name), type, FieldAttributes.Private);
            var property = value.DefineProperty(name, PropertyAttributes.HasDefault, type);

            var getMethod = value.DefineMethod("get_{0}".Formatting(name), MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, type);

            var il = getMethod.GetILGenerator();
            il.Ldarg().Ldfld(field.Builder).Ret();

            var setMethod = value.DefineMethod("set_{0}".Formatting(name), MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, type);
            il = setMethod.GetILGenerator();
            il.Ldarg().Ldarg(1).Stfld(field.Builder).Ret();

            property.Builder.SetGetMethod(getMethod.Builder);
            property.Builder.SetSetMethod(setMethod.Builder);


            return property;

        }

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


        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, string name, PropertyAttributes attributes, Type returnType)
        {
            return DefineProperty(value, name, attributes, returnType, null, null, null, null, null);
        }

        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
        {
            return DefineProperty(value, name, attributes, returnType, null, null, parameterTypes, null, null);
        }

        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
        {
            return DefineProperty(value, name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
        }


        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        {
            return DefineProperty(value, name, attributes, 0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
        }

        public static IPropertyConstructor DefineProperty(this ITypeConstructor value, string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
        {
            return EmitConstructor.Create(value.Context, value.Builder.DefineProperty
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
                ));
        }

        #endregion

        public static ITypeConstructor Create(this ITypeConstructor value, Action<Type> action = null)
        {
            if (value.Builder.IsNull()) return value;

            if (action.IsNull())
                value.Builder.CreateType();
            else
                action(value.Builder.CreateType());

            return value;
            //return value.Builder.False(e => e.IsNull(), e => e.CreateType());
        }


    }
}
