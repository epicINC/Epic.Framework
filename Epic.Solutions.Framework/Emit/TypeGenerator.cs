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
    public class TypeGenerator
    {
        // Epic.Security.Utility.RND()

        public static ITypeDefine Define()
        {
            return Define(new EmitConstructor().DefineAssembly(@"E:\Source\20110124 Epic.Framework\Source\Epic.Solutions.Framework.ConsoleApplication\bin\Debug\Epic.Framework.Dynamic.dll").DefineDynamicModule(), Epic.Security.Utility.RND());
        }

        public static ITypeDefine Define(ModuleBuilder builder, string name)
        {
            return Define(EmitConstructor.Create(builder), name);
        }

        public static ITypeDefine Define(IModuleConstructor value, string name)
        {
            return new TypeDefine() { Name = name, Context = value.Context };
        }


        public static ITypeConstructor Define(string name)
        {
            return Define(name, TypeAttributes.AnsiClass);
        }

        public static ITypeConstructor Define(string name, TypeAttributes attr, params Type[] inheritTypes)
        {
            if (inheritTypes.IsNullOrEmpty()) return Define(name, attr, null);
            Errors.CheckArgument(inheritTypes.Count(e => e.IsClass) > 1, "inheritTypes", "只能继承一个父类").Throw();

            return Define(name, attr, inheritTypes.SingleOrDefault(e => e.IsClass), inheritTypes.Where(e => e.IsInterface).ToArray());
        }

        public static ITypeConstructor Define(string name, TypeAttributes attr, Type parent, Type[] interfaces)
        {
            return Define(new EmitConstructor().DefineAssembly("Epic.Framework.Dynamic").DefineDynamicModule(), name, attr, parent, interfaces);
        }


        public static ITypeConstructor Define(IModuleConstructor value, string name, TypeAttributes attr, Type parent, Type[] interfaces)
        {
            return EmitConstructor.Create(value.Context, value.Context.Module.DefineType(name, attr, parent, interfaces));
        }

    }
}
