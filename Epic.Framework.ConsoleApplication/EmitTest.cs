using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Data.Common;
using System.Reflection;

namespace Epic.Framework.ConsoleApplication
{
    public static class EmitTest
    {

        public static void Test()
        {

            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Epic.Framework.DynamicTest";
            AppDomain domain = AppDomain.CurrentDomain;

            AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Epic.Framework.DynamicTest", "Epic.Framework.DynamicTest.dll");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicUpdater", TypeAttributes.Public);
            BuildMethodTest(typeBuilder);
            var newType = typeBuilder.CreateType();
            var mInfo = newType.GetMethod("Test");

            assemblyBuilder.Save("Epic.Framework.DynamicTest.dll");

        }


        public static MethodBuilder BuildMethodTest(TypeBuilder type)
        {
            // Declaring method builder
            // Method attributes
            System.Reflection.MethodAttributes methodAttributes =
                  System.Reflection.MethodAttributes.Private
                | System.Reflection.MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("Test", methodAttributes);
            // Preparing Reflection instances
            MethodInfo method1 = typeof(DbCommand).GetMethod(
                "get_Parameters",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method2 = typeof(DbParameterCollection).GetMethod(
                "get_Item",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            typeof(String)
            },
                null
                );
            MethodInfo method3 = typeof(RssItem).GetMethod(
                "get_ID",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method4 = typeof(DbParameter).GetMethod(
                "set_Value",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            typeof(Object)
            },
                null
                );
            MethodInfo method5 = typeof(RssItem).GetMethod(
                "get_FeedID",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method6 = typeof(RssItem).GetMethod(
                "get_CategoryID",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method7 = typeof(RssItem).GetMethod(
                "get_Title",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method8 = typeof(RssItem).GetMethod(
                "get_Content",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method9 = typeof(RssItem).GetMethod(
                "get_Url",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method10 = typeof(RssItem).GetMethod(
                "get_PubDate",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method11 = typeof(RssItem).GetMethod(
                "get_State",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method12 = typeof(RssItem).GetMethod(
                "get_CreateDate",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            MethodInfo method13 = typeof(DbCommand).GetMethod(
                "ExecuteNonQuery",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[]{
            },
                null
                );
            // Setting return type
            method.SetReturnType(typeof(void));
            // Adding parameters
            method.SetParameters(
                typeof(DbCommand),
                typeof(RssItem)
                );
            // Parameter command
            ParameterBuilder command = method.DefineParameter(1, ParameterAttributes.None, "command");
            // Parameter value
            ParameterBuilder value = method.DefineParameter(2, ParameterAttributes.None, "value");
            ILGenerator gen = method.GetILGenerator();
            // Writing body
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@ID");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method3);
            gen.Emit(OpCodes.Box, typeof(int));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@FeedID");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method5);
            gen.Emit(OpCodes.Box, typeof(int));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@CategoryID");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method6);
            gen.Emit(OpCodes.Box, typeof(int));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@Title");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method7);
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@Content");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method8);
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@Url");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method9);
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@PubDate");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method10);
            gen.Emit(OpCodes.Box, typeof(DateTime));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@State");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method11);
            gen.Emit(OpCodes.Box, typeof(RssItemState));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Ldstr, "@CreateDate");
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Callvirt, method12);
            gen.Emit(OpCodes.Box, typeof(DateTime));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Callvirt, method13);
            gen.Emit(OpCodes.Pop);
            gen.Emit(OpCodes.Ret);
            // finished
            return method;
        }


    }
}
