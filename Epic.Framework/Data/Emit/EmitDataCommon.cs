using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Common;
using System.Reflection.Emit;
using Epic.Data.Schema;
using Epic.Emit;

namespace Epic.Data.Emit
{

    internal static class EmitDataCommon
    {
        internal static Type DbCommand;
        internal static MethodInfo DbCommandGetParameters;
        internal static MethodInfo DbCommandExecuteNonQuery;

        internal static MethodInfo DbParameterCollectionGetItem;

        internal static MethodInfo DbParameterGetValue;
        internal static MethodInfo DbParameterSetValue;

        static EmitDataCommon()
        {
            DbCommand = typeof(DbCommand);
            var parameterCollection = typeof(System.Data.Common.DbParameterCollection);
            var parameter = typeof(System.Data.Common.DbParameter);

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;


            DbCommandGetParameters = DbCommand.GetMethod("get_Parameters", flags, null, new Type[] { }, null);
            DbCommandExecuteNonQuery = DbCommand.GetMethod("ExecuteNonQuery", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { }, null);

            DbParameterCollectionGetItem = parameterCollection.GetMethod("get_Item", flags, null, new Type[] { typeof(String) }, null);

            DbParameterGetValue = parameter.GetMethod("get_Value", flags, null, new Type[]{}, null);
            DbParameterSetValue = parameter.GetMethod("set_Value", flags, null, new Type[] { typeof(Object) }, null);
        }

        internal static void FillParameter(EmitGenerator il, ColumnSchema schema)
        {
            il.Ldarg(0)
                .Callvirt(EmitDataCommon.DbCommandGetParameters)
                .Ldstr("@" + schema.DbName)
                .Callvirt(EmitDataCommon.DbParameterCollectionGetItem)
                .Ldarg(1);

            if (schema.GetMethod != null)
                il.Callvirt(schema.GetMethod);
            else
                il.Stfld(schema.Field);
            if (schema.Type.IsValueType)
                il.Box(schema.Type);

            il.Callvirt(EmitDataCommon.DbParameterSetValue)
                .Nop();
        }


        internal static void FillParameter(ILGenerator il, ColumnSchema schema)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, EmitDataCommon.DbCommandGetParameters);
            il.Emit(OpCodes.Ldstr, "@" + schema.DbName);
            il.Emit(OpCodes.Callvirt, EmitDataCommon.DbParameterCollectionGetItem);
            il.Emit(OpCodes.Ldarg_1);
            if (schema.GetMethod != null)
                il.Emit(OpCodes.Callvirt, schema.GetMethod);
            else
                il.Emit(OpCodes.Stfld, schema.Field);

            if (schema.Type.IsValueType)
                il.Emit(OpCodes.Box, schema.Type);

            il.Emit(OpCodes.Callvirt, EmitDataCommon.DbParameterSetValue);
            il.Emit(OpCodes.Nop);
        }

    }
}
