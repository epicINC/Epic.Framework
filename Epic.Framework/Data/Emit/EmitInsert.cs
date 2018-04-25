using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection.Emit;
using Epic.Data.Schema;
using Epic.Emit;

namespace Epic.Data.Emit
{
    internal static class EmitInsert<T>
    {
        static Func<DbCommand, T, int> fill;

        internal static int Fill(DbCommand command, T value)
        {
            if (fill == null)
                fill = CreateDynamicMethod();
            return fill(command, value);
        }

        static Func<DbCommand, T, int> CreateDynamicMethod()
        {
            var type = typeof(T);
            var dynamicMethod = new DynamicMethod("Epic.Data.Common.Insert" + type.Name, typeof(int), new Type[] { typeof(DbCommand), type }, type, true);
            var il = dynamicMethod.GetILGenerator();

            GenerateIL(il);
            return (Func<DbCommand, T, int>)dynamicMethod.CreateDelegate(typeof(Func<DbCommand, T, int>));


            var builder = new DynamicMethodBuilder("Epic.Data.Common.Insert" + type.Name, typeof(int), EmitDataCommon.DbCommand, type);
            GenerateIL(builder);
            return builder.CreateDelegate<Func<DbCommand, T, int>>();

        }


        static void GenerateIL(EmitGenerator il)
        {
            il.DeclareLocal<int>();
            il.Nop();
            foreach (var schema in TableSchema<T>.Columns)
            {
                EmitDataCommon.FillParameter(il, schema);
            }
            il.Ldarg(0)
                .Callvirt(EmitDataCommon.DbCommandExecuteNonQuery)
                .Stloc(0);

            if (TableSchema<T>.PrimaryKeys.Count > 0)
            {
                il.Ldarg(1)
                    .Ldarg(0)
                    .Callvirt(EmitDataCommon.DbCommandGetParameters)
                    .Ldstr("@" + TableSchema<T>.PrimaryKeys[0].DbName)
                    .Callvirt(EmitDataCommon.DbParameterCollectionGetItem)
                    .Callvirt(EmitDataCommon.DbParameterGetValue)
                    .UnboxAny<int>()
                    .Callvirt(TableSchema<T>.PrimaryKeys[0].SetMethod)
                    .Nop();
            }

            il.Ldloc(0)
                .Ret();

        }

        static void GenerateIL(ILGenerator il)
        {
            LocalBuilder result = il.DeclareLocal(typeof(int));


            il.Emit(OpCodes.Nop);

            foreach (var schema in TableSchema<T>.Columns)
            {
                EmitDataCommon.FillParameter(il, schema);
            }

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, EmitDataCommon.DbCommandExecuteNonQuery);
            il.Emit(OpCodes.Stloc_0);


            

            if (TableSchema<T>.PrimaryKeys.Count > 0)
            {
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, EmitDataCommon.DbCommandGetParameters);
                il.Emit(OpCodes.Ldstr, "@" + TableSchema<T>.PrimaryKeys[0].DbName);
                il.Emit(OpCodes.Callvirt, EmitDataCommon.DbParameterCollectionGetItem);
                il.Emit(OpCodes.Callvirt, EmitDataCommon.DbParameterGetValue);
                il.Emit(OpCodes.Unbox_Any, typeof(int));
                il.Emit(OpCodes.Callvirt, TableSchema<T>.PrimaryKeys[0].SetMethod);
                il.Emit(OpCodes.Nop);
            }

            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

    }
}
