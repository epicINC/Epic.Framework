using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using System.Data.Common;
using Epic.Data.Schema;
using Epic.Emit;


namespace Epic.Data.Emit
{
    #region Update Common



    #endregion

    /// <summary>
    /// EmitUpdate
    /// </summary>
    internal static class EmitUpdate<T>
    {
        static Func<DbCommand, T, int> fill;

        internal static int Fill(DbCommand command, T value)
        {
            if (fill == null)
            {
                fill = CreateDynamicMethod();
            }
            return fill(command, value);
        }

        static Func<DbCommand, T, int> CreateMethod()
        {
            var type = typeof(T);

            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "Epic.Framework.DynamicUpdate";
            AppDomain domain = AppDomain.CurrentDomain;

            AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Epic.Framework.DynamicUpdate", "Epic.Framework.DynamicUpdate.dll");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("DynamicUpdater", TypeAttributes.Public);

            var methodBuilder = typeBuilder.DefineMethod("Update" + type.Name, MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, typeof(int), new Type[] { typeof(DbCommand), type });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "command");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "item");


            ILGenerator il = methodBuilder.GetILGenerator();
            GenerateIL(il);

            var newType = typeBuilder.CreateType();
            var mInfo = newType.GetMethod("Update" + type.Name);


            assemblyBuilder.Save("Epic.Framework.DynamicUpdate.dll");

            return (Func<DbCommand, T, int>)Delegate.CreateDelegate(typeof(Func<DbCommand, T, int>), mInfo);


        }





        static Func<DbCommand, T, int> CreateDynamicMethod()
        {
            var type = typeof(T);
            //var dynamicMethod = new DynamicMethod("Epic.Data.Common.Update" + type.Name, typeof(int), new Type[] { typeof(DbCommand), type }, type, true);
            //var il = dynamicMethod.GetILGenerator();

            //GenerateIL(il);
            //return (Func<DbCommand, T, int>)dynamicMethod.CreateDelegate(typeof(Func<DbCommand, T, int>));


            var builder = new DynamicMethodBuilder("Epic.Data.Common.Update" + type.Name, typeof(int), typeof(DbCommand), type);
            GenerateIL(builder);
            return builder.CreateDelegate<Func<DbCommand, T, int>>();

 
        }

        static void GenerateIL(EmitGenerator il)
        {
            il.DeclareLocal<int>()
                .Nop();

            foreach (var schema in TableSchema<T>.Columns)
            {
                EmitDataCommon.FillParameter(il, schema);
            }

            il.Ldarg(0)
                .Callvirt(EmitDataCommon.DbCommandExecuteNonQuery)
                .Stloc(0)
                .Ldloc(0)
                .Ret();
        }


        static void GenerateIL(ILGenerator il)
        {
            LocalBuilder result =  il.DeclareLocal(typeof(int));


            il.Emit(OpCodes.Nop);

            foreach (var schema in TableSchema<T>.Columns)
            {
                EmitDataCommon.FillParameter(il, schema);
            }

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, EmitDataCommon.DbCommandExecuteNonQuery);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);

            il.Emit(OpCodes.Ret);
        }



    }
}
