using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Epic.Emit;
using Epic.Data.Schema;
using System.Data;
using System.Data.Common;
using Epic.Data.SqlMapper;

namespace Epic.Data.Mapper
{
    internal class EmitSqlDataMapper<T> : DataMapper<DbDataReader, T> where T : class
    {
        bool userCache;

        internal EmitSqlDataMapper(bool cacheAble = true)
        {
            this.userCache = cacheAble;
            this.Definition = new SqlDbDefinition();

        }

        Type ModelType = typeof(T);

        Func<SqlCommand, T, int> Init(Func<SqlCommand, T, int> method, Func<Func<SqlCommand, T, int>> initMethod)
        {
            if (!this.userCache)
                return initMethod();
            if (method == null)
                method = initMethod();
            return method;
        }


        #region Insert Method

        internal Func<SqlCommand, T, int> CreateInsert()
        {
            var il = new DynamicMethodBuilder("Epic.Data.Common.Insert" + this.ModelType.Name, this.Definition.Int, this.Definition.Command, this.ModelType);

            il.DeclareLocal<int>();
            il.Nop();
            foreach (var schema in TableDefinition<T>.Columns.Values)
            {
                this.FillParameter(il, schema);
            }
            il.Ldarg(0)
                .Callvirt(this.Definition.ExecuteNonQuery)
                .Stloc(0);

            if (TableDefinition<T>.PrimaryKeys.Count > 0)
            {
                il.Ldarg(1)
                    .Ldarg(0)
                    .Callvirt(this.Definition.GetParameters)
                    .Ldstr("@" + TableDefinition<T>.PrimaryKeys[0].ColumnName)
                    .Callvirt(this.Definition.ParameterCollectionGetItem)
                    .Callvirt(this.Definition.ParameterGetValue)
                    .UnboxAny<int>()
                    .Callvirt(TableDefinition<T>.PrimaryKeys[0].Property.GetSetMethod())
                    .Nop();
            }

            il.Ldloc(0)
                .Ret();

            return il.CreateDelegate<Func<SqlCommand, T, int>>();
        }



        internal static Func<SqlCommand, T, int> InsertMethod;

        public  int Insert(SqlCommand command, T value)
        {
            return this.Init(InsertMethod, this.CreateInsert)(command, value);

            if (InsertMethod == null)
                InsertMethod = this.CreateInsert();
            return InsertMethod(command, value);
        }

        public override int Insert(DbCommand command, T value)
        {
            return this.Insert(command as SqlCommand, value);
        }

        #endregion

        #region Update Method

        internal Func<SqlCommand, T, int> CreateUpdate()
        {

            var il = new DynamicMethodBuilder("Epic.Data.Common.Update" + this.ModelType.Name, typeof(int), typeof(DbCommand), this.ModelType);
            il.DeclareLocal<int>()
                .Nop();

            foreach (var schema in TableDefinition<T>.Columns.Values)
            {
                this.FillParameter(il, schema);
            }

            il.Ldarg(0)
                .Callvirt(this.Definition.ExecuteNonQuery)
                .Stloc(0)
                .Ldloc(0)
                .Ret();
            return il.CreateDelegate<Func<SqlCommand, T, int>>();
        }

        internal static Func<SqlCommand, T, int> UpdateMethod;

        public int Update(SqlCommand command, T value)
        {
            return this.Init(UpdateMethod, this.CreateUpdate)(command, value);
            if (UpdateMethod == null)
                UpdateMethod = this.CreateUpdate();
            return UpdateMethod(command, value);
        }

        public override int Update(DbCommand command, T value)
        {
            return this.Update(command as SqlCommand, value);
        }

        #endregion

        #region Delete Method

        Func<SqlCommand, T, int> CreateDelete()
        {
            var il = new DynamicMethodBuilder("Epic.Data.Common.Update" + this.ModelType.Name, this.Definition.Int, this.Definition.Command, this.ModelType);
            il.DeclareLocal<int>()
                .Nop();

            if (TableDefinition<T>.PrimaryKeys.Count > 0)
            {
                il.Ldarg(0)
                    .Callvirt(this.Definition.GetParameters)
                    .Ldstr("@" + TableDefinition<T>.PrimaryKeys[0].ColumnName)
                    .Callvirt(this.Definition.ParameterCollectionGetItem)
                    .Ldarg(1)
                    .Callvirt(TableDefinition<T>.PrimaryKeys[0].Property.GetGetMethod())
                    .Box<int>()
                    .Callvirt(this.Definition.ParameterSetValue)
                    .Nop();

            }

            il.Ldarg(0)
                .Callvirt(this.Definition.ExecuteNonQuery)
                .Stloc(0)
                .Ldloc(0)
                .Ret();

            return il.CreateDelegate<Func<SqlCommand, T, int>>();
        }

        internal static Func<SqlCommand, T, int> DeleteMethod;

        public int Delete(SqlCommand command, T value)
        {
            return this.Init(DeleteMethod, this.CreateDelete)(command, value);
            if (DeleteMethod == null)
                DeleteMethod = this.CreateDelete();
            return DeleteMethod(command, value);
        }

        public override int Delete(DbCommand command, T value)
        {
            return this.Delete(command as SqlCommand, value);
        }

        #endregion

        #region Reader Method(Convert)


        static SortedList<int, ColumnDefinition> GetOrdina(DbDataReader dr)
        {
            var result = new SortedList<int, ColumnDefinition>();
            foreach (var item in TableDefinition<T>.Columns.Values)
            {
                result.Add(dr.GetOrdinal(item.ColumnName), item);
            }

            return result;


        }

        internal Func<DbDataReader, T> CreateConvert(SortedList<int, ColumnDefinition> ordinal)
        {
            var il = new DynamicMethodBuilder("Epic.Data.Common.Parse" + this.ModelType.Name, this.ModelType, this.Definition.Reader);
            il.DeclareLocal(this.ModelType)
                .DeclareLocal(this.Definition.Boolean)
                .Nop()
                .Newobj(this.ModelType)
                .Stloc(0);
            foreach (var item in ordinal)
            {
                this.FillReaderField(this.ModelType, il, item);
            }

            il.Ldloc(0)
                .Ret();

            return il.CreateDelegate<Func<DbDataReader, T>>();
        }

        static Func<DbDataReader, T> ConvertMethod;

        public override T Convert(DbDataReader value)
        {
            if (ConvertMethod == null)
                ConvertMethod = CreateConvert(GetOrdina(value));

            return ConvertMethod(value);
        }

        #endregion






    }
}
