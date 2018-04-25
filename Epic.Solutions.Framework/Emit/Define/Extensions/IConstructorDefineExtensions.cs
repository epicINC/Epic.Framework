using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Emit
{
    public static class IConstructorDefineExtensions
    {

        #region Attributes

        public static IConstructorDefine DefineAttributes(this IConstructorDefine value, MethodAttributes attributes)
        {
            value.Attributes |= attributes;
            return value;
        }

        public static IConstructorDefine Abstract(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Abstract);
        }

        public static IConstructorDefine Internal(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Assembly);
        }

        public static IConstructorDefine Protected(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Family);
        }

        public static IConstructorDefine Final(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Final);
        }

        public static IConstructorDefine HasSecurity(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.HasSecurity);
        }

        public static IConstructorDefine HideBySig(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.HideBySig);
        }

        public static IConstructorDefine Private(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Private);
        }

        public static IConstructorDefine Public(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Public);
        }

        public static IConstructorDefine RTSpecialName(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.RTSpecialName);
        }

        public static IConstructorDefine SpecialName(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.SpecialName);
        }

        public static IConstructorDefine Static(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Static);
        }

        public static IConstructorDefine UnmanagedExport(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.UnmanagedExport);
        }

        public static IConstructorDefine Virtual(this IConstructorDefine value)
        {
            return DefineAttributes(value, MethodAttributes.Virtual);
        }

        #endregion

        public static IConstructorDefine DefineCallingConvention(this IConstructorDefine value, CallingConventions callingConventions)
        {
            value.CallingConvention = callingConventions;
            return value;
        }

        public static IConstructorDefine DefineParameterTypes<T>(this IConstructorDefine value, params Type[] parameterTypes)
        {
            return DefineParameterTypes(value, TypesPool.Typeof<T>());
        }

        public static IConstructorDefine DefineParameterTypes<T, K>(this IConstructorDefine value, params Type[] parameterTypes)
        {
            return DefineParameterTypes(value, TypesPool.Typeof<T>(), TypesPool.Typeof<K>());
        }

        public static IConstructorDefine DefineParameterTypes<T, K, L>(this IConstructorDefine value, params Type[] parameterTypes)
        {
            return DefineParameterTypes(value, TypesPool.Typeof<T>(), TypesPool.Typeof<K>(), TypesPool.Typeof<L>());
        }

        public static IConstructorDefine DefineParameterTypes(this IConstructorDefine value, params Type[] parameterTypes)
        {
            value.ParameterTypes = parameterTypes;
            return value;
        }

        public static IConstructorDefine DefineRequiredCustomModifiers(this IConstructorDefine value, params Type[][] requiredCustomModifiers)
        {
            value.RequiredCustomModifiers = requiredCustomModifiers;
            return value;
        }

        public static IConstructorDefine DefineOptionalCustomModifiers(this IConstructorDefine value, params Type[][] optionalCustomModifiers)
        {
            value.OptionalCustomModifiers = optionalCustomModifiers;
            return value;
        }


        public static ICtorConstructor Generator(this IConstructorDefine value)
        {
            Errors.CheckArgumentNull(value.Context.Type, "type", "Type 为空").Throw();

            if (value.ParameterTypes == null)
                value.ParameterTypes = Type.EmptyTypes;

            return EmitConstructor.Create(value.Context, value.Context.Type.DefineConstructor(value.Attributes, value.CallingConvention, value.ParameterTypes, value.RequiredCustomModifiers, value.OptionalCustomModifiers));
        }


        public static ICtorConstructor Default(this ICtorConstructor value)
        {
            var il = value.GetILGenerator();
            il.Ldarg().Call<object>().Ret();

            return value;
        }

    }
}
