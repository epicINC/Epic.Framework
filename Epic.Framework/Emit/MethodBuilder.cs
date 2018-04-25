using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Epic.Emit
{
    public sealed class MethodBuilder1 : EmitGenerator
    {

        Type returnType;
        AssemblyBuilder assembly;
        TypeBuilder type;
        MethodBuilder method;

        public MethodBuilder1(string name, Type returnType, params Type[] parameterTypes)
        {
            this.returnType = returnType;

            var assemblyName = new AssemblyName();
            assemblyName.Name = "Epic.Framework.Dynamic";
            var domin = AppDomain.CurrentDomain;
            this.assembly = domin.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var module = assembly.DefineDynamicModule("Epic.Framework.Dynamic", "Epic.Framework.Dynamic.dll");
            this.type = module.DefineType("DynamicReader", TypeAttributes.Public);
            Type[] args = { typeof(System.Data.Common.DbDataReader) };
            this.method = this.type.DefineMethod("Parse" + returnType.Name, MethodAttributes.Public | MethodAttributes.Static, returnType, args);
            method.DefineParameter(1, ParameterAttributes.None, "dr");
            base.il = new ILGeneratorWrapper(method.GetILGenerator());

        }

        public T CreateDelegate<T>() where T : class
        {

            var newType = this.type.CreateType();
            var mInfo = newType.GetMethod("Parse" + returnType.Name);


            this.assembly.Save("Epic.Framework.Dynamic.dll");


            return Delegate.CreateDelegate(typeof(T), mInfo) as T;


            //return this.method.CreateDelegate(typeof(T)) as T;
        }
    }
}
