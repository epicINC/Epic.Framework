using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using System.Reflection.Emit;
using System.Reflection;

namespace Epic.Emit
{
    public static class ITypeDefineExtensions
    {

        #region Attributes

        public static ITypeDefine DefineAttributes(this ITypeDefine value, TypeAttributes attributes)
        {
            value.Attributes |= attributes;
            return value;
        }

        public static ITypeDefine Class(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.Class);
        }

        public static ITypeDefine Interface(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.Interface);
        }

        public static ITypeDefine Public(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.Public);
        }

        public static ITypeDefine Private(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.NotPublic);
        }

        public static ITypeDefine Internal(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.NestedAssembly);
        }

        public static ITypeDefine Protected(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.NestedFamily);
        }

        public static ITypeDefine BeforeFieldInit(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.BeforeFieldInit);
        }

        public static ITypeDefine Abstract(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.Abstract);
        }

        public static ITypeDefine Ansi(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.AnsiClass);
        }
        public static ITypeDefine Unicode(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.UnicodeClass);
        }
        public static ITypeDefine Auto(this ITypeDefine value)
        {
            return DefineAttributes(value, TypeAttributes.AutoClass);
        }

        #endregion

        public static ITypeDefine Inherit<T>(this ITypeDefine value)
        {
            return Inherit(value, TypesPool.Typeof<T>());
        }

        public static ITypeDefine Inherit<T, K>(this ITypeDefine value)
        {
            return Inherit(value, TypesPool.Typeof<T>(), TypesPool.Typeof<K>());
        }

        public static ITypeDefine Inherit<T, K, L>(this ITypeDefine value)
        {
            return Inherit(value, TypesPool.Typeof<T>(), TypesPool.Typeof<K>(), TypesPool.Typeof<L>());
        }

        public static ITypeDefine Inherit(this ITypeDefine value, params Type[] inheritTypes)
        {
            if (inheritTypes.IsNullOrEmpty()) return value;
            Errors.CheckArgument(inheritTypes.Count(e => e.IsClass) > 1, "inheritTypes", "只能继承一个父类").Throw();

            value.Parent = inheritTypes.SingleOrDefault(e => e.IsClass);
            value.Interfaces = inheritTypes.Where(e => e.IsInterface).ToArray();

            return value;
        }

        public static ITypeConstructor Generator(this ITypeDefine value)
        {
            Errors.CheckArgumentNull(value.Context.Module, "module", "module 为空").Throw();
            return EmitConstructor.Create(value.Context, value.Context.Module.DefineType(value.Name, value.Attributes, value.Parent, value.Interfaces));
        }

    }
}
