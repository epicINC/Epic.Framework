using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Epic.Emit;
using Epic.Data.Schema;
using System.Data;


namespace Epic.Data.Mapper
{




    public abstract class DataMapper<Source, Dest> : IDataMapper<Source, Dest>
        where Source : System.Data.IDataReader
        where Dest : class
    {

        public abstract int Insert(DbCommand command, Dest value);
        public abstract int Update(DbCommand command, Dest value);
        public abstract int Delete(DbCommand command, Dest value);
        public abstract Dest Convert(Source value);

        protected DbTypeDefinition Definition
        {
            get;
            set;
        }

        protected void FillParameter(EmitGenerator il, ColumnDefinition schema)
        {
            il.Ldarg(0)
                .Callvirt(this.Definition.GetParameters)
                .Ldstr("@" + schema.ColumnName)
                .Callvirt(this.Definition.ParameterCollectionGetItem)
                .Ldarg(1);


                il.Callvirt(schema.Property.GetGetMethod());

            if (schema.Property.DeclaringType.IsValueType)
                il.Box(schema.Property.DeclaringType);

            il.Callvirt(this.Definition.ParameterSetValue)
                .Nop();
        }

        protected void FillReaderField(Type modelType, EmitGenerator il, KeyValuePair<int, ColumnDefinition> pair)
        {
            
            var index = pair.Key;
            var column = pair.Value;



            il.IF(column.Optional, e => e.Ldarg(0)
                    .Ldloc(index)
                    .Callvirt(this.Definition.IsDBNull)
                    .Stloc(1)
                    .Ldloc(1)
                    .Brtrue_S())
                    .Ldloc(0)
                    .Ldarg(0)
                    .Ldc(index);


            FillReaderFieldMethod(il, column.Property.PropertyType);

            il.Callvirt(column.Property.GetSetMethod())
                .Nop()
                .IF(column.Optional, e => e.MarkLabel());




        }

        protected void FillReaderFieldMethod(EmitGenerator il, Type fieldType)
        {


            if (fieldType.IsEnum)
            {
                il.Callvirt(this.Definition.GetValue)
                    .UnboxAny(fieldType);
                return;
            }

            switch (Type.GetTypeCode(fieldType))
            {
                case TypeCode.Boolean:
                    il.Callvirt(this.Definition.GetBoolean);
                    return;
                case TypeCode.Byte:
                    il.Callvirt(this.Definition.GetByte);
                    return;
                case TypeCode.Char:
                    il.Callvirt(this.Definition.GetChar);
                    return;
                case TypeCode.DateTime:
                    il.Callvirt(this.Definition.GetDateTime);
                    return;
                case TypeCode.Decimal:
                    il.Callvirt(this.Definition.GetDecimal);
                    return;
                case TypeCode.Double:
                    il.Callvirt(this.Definition.GetDouble);
                    return;
                case TypeCode.Int16:
                    il.Callvirt(this.Definition.GetInt16);
                    return;
                case TypeCode.Int32:
                    il.Callvirt(this.Definition.GetInt32);
                    return;
                case TypeCode.Int64:
                    il.Callvirt(this.Definition.GetInt64);
                    return;
                case TypeCode.SByte:
                    il.Callvirt(this.Definition.GetValue)
                        .UnboxAny(this.Definition.SByte);
                    return;
                case TypeCode.Single:
                    il.Callvirt(this.Definition.GetValue)
                        .UnboxAny(this.Definition.Single);
                    return;
                case TypeCode.String:
                    il.Callvirt(this.Definition.GetString);
                    return;
                case TypeCode.UInt16:
                    il.Callvirt(this.Definition.GetValue)
                        .UnboxAny(this.Definition.UInt16);
                    return;
                case TypeCode.UInt32:
                    il.Callvirt(this.Definition.GetValue)
                        .UnboxAny(this.Definition.UInt32);
                    return;
                case TypeCode.UInt64:
                    il.Callvirt(this.Definition.GetValue)
                        .UnboxAny(this.Definition.UInt64);
                    return;
            }


            if (fieldType == this.Definition.Guid)
            {
                il.Callvirt(this.Definition.GetValue)
                    .UnboxAny(this.Definition.Guid);
                return;
            }

            if (fieldType == this.Definition.ByteArray)
            {
                il.Callvirt(this.Definition.GetValue)
                    .UnboxAny(this.Definition.ByteArray);
                return;
            }

            if (fieldType == this.Definition.DateTimeOffset)
            {
                il.Callvirt(this.Definition.GetValue)
                    .UnboxAny(this.Definition.DateTimeOffset);
                return;
            }

            throw new ApplicationException(String.Format("未知 Type: {0}", fieldType));

        }



        int IDataMapper<Source, Dest>.Insert(IDbCommand command, Dest value)
        {
            return this.Insert(command as DbCommand, value);
        }

        int IDataMapper<Source, Dest>.Update(IDbCommand command, Dest value)
        {
            return this.Update(command as DbCommand, value);
        }

        int IDataMapper<Source, Dest>.Delete(IDbCommand command, Dest value)
        {
            return this.Delete(command as DbCommand, value);
        }



    }
}
