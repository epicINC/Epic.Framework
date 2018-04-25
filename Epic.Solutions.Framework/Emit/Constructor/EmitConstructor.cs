using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Epic.Extensions;

namespace Epic.Emit
{
    public interface IConstructor
    {
        IEmitContext Context
        {
            get;
            set;
        }
    }

    public class EmitConstructor : IConstructor
    {

        #region Create

        public static T Create<T>(IConstructor value) where T : IConstructor, new()
        {
            return Create<T>(value.Context);
        }

        public static T Create<T>(IEmitContext value) where T : IConstructor, new()
        {
            return new T() { Context = value };
        }

        static T Create<T, K>(IEmitContext context, K builder) where T : BaseBuilderConstructor<K>, new()
        {
            return new T().Func(e => { e.Builder = builder; e.Context = context; return e; });
        }


        public static IAssemblyConstructor Create(AssemblyBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static IAssemblyConstructor Create(IEmitContext context, AssemblyBuilder builder)
        {
            context.Assembly = builder;
            return Create<AssemblyConstructor, AssemblyBuilder>(context, builder);
        }

        public static IModuleConstructor Create(ModuleBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static IModuleConstructor Create(IEmitContext context, ModuleBuilder builder)
        {
            context.Module = builder;
            return Create<ModuleConstructor, ModuleBuilder>(context, builder);
        }

        public static ITypeConstructor Create(TypeBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static ITypeConstructor Create(IEmitContext context, TypeBuilder builder)
        {
            context.Type = builder;
            return Create<TypeConstructor, TypeBuilder>(context, builder);
        }

        public static ICtorConstructor Create(ConstructorBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static ICtorConstructor Create(IEmitContext context, ConstructorBuilder builder)
        {
            context.Ctor = builder;
            return Create<CtorConstructor, ConstructorBuilder>(context, builder);
        }

        public static IFieldConstructor Create(FieldBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static IFieldConstructor Create(IEmitContext context, FieldBuilder builder)
        {
            context.Field = builder;
            return Create<FieldConstructor, FieldBuilder>(context, builder);
        }

        public static IPropertyConstructor Create(PropertyBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static IPropertyConstructor Create(IEmitContext context, PropertyBuilder builder)
        {
            context.Property = builder;
            return Create<PropertyConstructor, PropertyBuilder>(context, builder);
        }

        public static IMethodConstructor Create(MethodBuilder builder)
        {
            return Create(new EmitContext(), builder);
        }

        public static IMethodConstructor Create(IEmitContext context, MethodBuilder builder)
        {
            context.Method = builder;
            return Create<MethodConstructor, MethodBuilder>(context, builder);
        }

        #endregion




        public EmitConstructor()
        {
            this.Context = new EmitContext();
        }

        public IEmitContext Context
        {
            get;
            set;
        }

    }
}
