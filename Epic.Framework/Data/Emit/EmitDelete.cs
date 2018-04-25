using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Epic.Data.Schema;
using Epic.Emit;

namespace Epic.Data.Emit
{
    internal static class EmitDelete<T>
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


        static Func<DbCommand, T, int> CreateDynamicMethod()
        {
            var type = typeof(T);
            var builder = new DynamicMethodBuilder("Epic.Data.Common.Update" + type.Name, typeof(int), typeof(DbCommand), type);
            GenerateIL(builder);
            return builder.CreateDelegate<Func<DbCommand, T, int>>();
        }

        static void GenerateIL(EmitGenerator il)
        {
            il.DeclareLocal<int>()
                .Nop();

            if (TableSchema<T>.PrimaryKeys.Count > 0)
            {
                il.Ldarg(0)
                    .Callvirt(EmitDataCommon.DbCommandGetParameters)
                    .Ldstr("@" + TableSchema<T>.PrimaryKeys[0].DbName)
                    .Callvirt(EmitDataCommon.DbParameterCollectionGetItem)
                    .Ldarg(1)
                    .Callvirt(TableSchema<T>.PrimaryKeys[0].GetMethod)
                    .Box<int>()
                    .Callvirt(EmitDataCommon.DbParameterSetValue)
                    .Nop();

            }

            il.Ldarg(0)
                .Callvirt(EmitDataCommon.DbCommandExecuteNonQuery)
                .Stloc(0)
                .Ldloc(0)
                .Ret();

        }
    }
}
