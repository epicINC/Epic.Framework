using Epic.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;

namespace Epic.Solutions.Framework.ConsoleApplication.Emit
{
    public static class TypeGeneratorTest
    {
        public static void Test()
        {

            //AssemblyName assemblyName = new AssemblyName();
            //assemblyName.Name = "HelloWorld";
            //// 定义动态程序集
            //AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            //ModuleBuilder moduleBuider = assemblyBuilder.DefineDynamicModule("HelloWorld");

            //// 创建类型
            //TypeBuilder typeBuilder = moduleBuider.DefineType("HelloWord", TypeAttributes.Public | TypeAttributes.Class);
            //// 创建Main方法
            //MethodBuilder methodBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.Static, typeof(void), new Type[] { typeof(string[]) });

            //// 为Main方法创建IL代码
            //ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            //ilGenerator.EmitWriteLine("Hello,World!");
            //ilGenerator.Emit(OpCodes.Ret);

            //// 创建实例
            //Type helloWorldType = typeBuilder.CreateType();
            //// 调用方法
            //helloWorldType.GetMethod("Main").Invoke(null, new string[] { null });

            //// 创建入口点
            //// 保存
            //assemblyBuilder.Save("HelloWorld.exe");




            TypeGenerator.Define()
                .Public().Class().Inherit<User>().Generator()
                .DefineCtor().Public().Generator().Default()
               // .DefinePropertyAutoImplemented<int>("_id")
               // .DefinePropertyAutoImplemented<DateTime>("FullName")
                .TypeConstructor()
               .Func(e => { e.Create(); return e; })
                .AssemblyConstructor().Save();

        }


    }
}
