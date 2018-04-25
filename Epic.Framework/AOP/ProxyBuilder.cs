using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Epic.Emit;
using Epic.Extensions;

namespace Epic.AOP
{
    /// <summary>
    /// http://www.cnblogs.com/kiminozo/archive/2011/12/17/2291148.html
    /// </summary>
    public class ProxyBuilder
    {

        public static T Create<T>()
        {
            return (T)(T)Activator.CreateInstance(BulidType<T>(typeof(T)));
        }

        static Type BulidType<T>(Type type)
        {
            var constructor = new EmitConstructor().DefineAssembly(@"E:\Source\20110124 Epic.Framework\Source\Publish\Epic.Framework.Dynamic.dll")
                .DefineDynamicModule("Epic.Framework.Dynamic", "Epic.Framework.Dynamic.dll")
                .DefineType(type.Name + "_Proxy", TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, type)
                .DefineField("inspector", typeof(IInterceptor), FieldAttributes.Private | FieldAttributes.InitOnly);

            var inspector = constructor.Builder;

            BuildCtor<T>(constructor.GetTypeConstructor(), type, inspector);
            BuildMethod(constructor, inspector, type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(e =>
                !e.IsSpecialName || !(e.Name.StartsWith("set_") || e.Name.StartsWith("get_"))
                ));
            BuildProperty(constructor, inspector, type.GetProperties());
            var result = constructor.Context.Type.Builder.CreateType();
            constructor.GetAssemblyConstructor().Save("Epic.Framework.Dynamic.dll");

            return result;
        }

        static void BuildCtor<T>(ITypeConstructor constructor, Type type, FieldBuilder field)
        {
            constructor.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis).GetILGenerator()
                .Ldarg()
                .Call(type.GetConstructor(Type.EmptyTypes))
                .Ldarg()
                .Call(typeof(InterceptorFactory<T>).GetMethod("Create"))
                .Stfld(field)
                .Ret();
        }

        static void BuildProperty(IFieldConstructor constructor, FieldBuilder inspector, IEnumerable<PropertyInfo> properties)
        {
            properties.ForEach(e => BuildProperty(constructor, inspector, e));
        }

        static void BuildProperty(IFieldConstructor constructor, FieldBuilder inspector, PropertyInfo property)
        {
            var builder = constructor.GetTypeConstructor().DefineProperty(property).Builder;

            builder.SetGetMethod(BuildMethod(constructor, inspector, property.GetGetMethod()));
            builder.SetSetMethod(BuildMethod(constructor, inspector, property.GetSetMethod()));
        }

        static void BuildMethod(IFieldConstructor constructor, FieldBuilder inspector, IEnumerable<MethodInfo> methods)
        {
            methods.ForEach(e => BuildMethod(constructor, inspector, e));
        }

        static MethodBuilder BuildMethod(IFieldConstructor constructor, FieldBuilder inspector, MethodInfo method)
        {
            return BuildMethod(constructor, inspector, method, method.GetParameters());
        }

        static MethodBuilder BuildMethod(IFieldConstructor constructor, FieldBuilder inspector, MethodInfo method, ParameterInfo[] parameters)
        {
            return BuildMethod(constructor, inspector, method, parameters, parameters.Select(e => e.ParameterType).ToArray());
        }

        static MethodBuilder BuildMethod(IFieldConstructor constructor, FieldBuilder inspector, MethodInfo method, ParameterInfo[] parameters, Type[] parameterTypes)
        {
            constructor.GetTypeConstructor().DefineMethod(method).GetILGenerator()
                .DeclareLocal(TypesPool.Object, "state")
                .DeclareLocal(method.ReturnType, "result")
                .DeclareLocal(TypesPool.ObjectArray)
                .Ldarg(0)
                .Ldfld(inspector)
                .Ldstr(method.Name)
                .IIF(parameterTypes.Count() == 0, e => e.Ldnull(), e =>
                {
                    e.Ldc(parameterTypes.Count())
                        .Newarr(TypesPool.Object)
                        .Stloc(2)
                        .For(parameterTypes, (index, value) =>
                        {
                            e.Ldloc(2)
                            .Ldc(index)
                            .Ldarg(index + 1)
                            .IF(value.IsValueType, () => e.Box(value))
                            .Stelem_Ref();
                        })
                        .Ldloc(2);
                })
                // BeforeCall
                .Callvirt(typeof(IMethodInterceptor).GetMethod("BeforeCall"))
                .Stloc(0)
                .Ldarg(0)
                .For(parameterTypes, (e, index) => e.Ldarg(index + 1))
                .Call(method)
                //.IFElse(method.ReturnType == TypesPool.Void, e => e.Ldnull(), method.ReturnType.IsValueType, e => e.Box(method.ReturnType))
                .IF(method.ReturnType != TypesPool.Void, e => { e.Stloc(1); })

                // AfterCall
                .Ldarg(0)
                .Ldfld(inspector)
                .Ldstr(method.Name)

                .IF(method.ReturnType != TypesPool.Void, e =>
                {
                    e.Ldloc(1);
                    e.IF(method.ReturnType.IsValueType, () => e.Box(method.ReturnType));
                }, e => e.Ldnull())
                .Ldloc(0)
                .Callvirt(typeof(IMethodInterceptor).GetMethod("AfterCall"))
                .Nop()
                .IF(method.ReturnType == TypesPool.Void, e => e.Ret().Break())
                .Ldloc(1)
                .Ret();
            return constructor.Context.Method.Builder;

        }


    }
}
