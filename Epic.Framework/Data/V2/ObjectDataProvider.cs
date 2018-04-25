using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Common;
using System.Configuration;
using System.Data;

namespace Epic.Data.V2
{
    public class ObjectDataProvider<T>
    {

        public ObjectDataProvider() : this(0)
        {
        }

        public ObjectDataProvider(int i)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[i].ConnectionString;
        }

        public ObjectDataProvider(string connectionString)
        {
            if (connectionString.IndexOf("Data Source") != -1)
                this.connectionString = connectionString;
            else
                ReaderConnectionStringConfig(connectionString);

        }

        public ObjectDataProvider(DbConnection connection)
        {
            this.connection = connection;
            this.connectionString = connection.ConnectionString;
        }

        void ReaderConnectionStringConfig(string name)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }


        #region Connection

        string connectionString;
        DbConnection connection;

        public string ConnectionString
        {
            get { return this.connectionString; }
        }

        internal void EnsureConnection()
        {
            if (this.connection == null)
                this.connection = new System.Data.SqlClient.SqlConnection(this.connectionString);

            if (this.connection.State == ConnectionState.Closed)
                this.connection.Open();
        }


        internal void ReleaseConnection()
        {
            if (this.connection == null)
                return;

            if (this.connection.State != ConnectionState.Closed)
                this.connection.Close();

        }

        #endregion

        #region Command

        public DbCommand CreateCommand()
        {
            this.EnsureConnection();
            return this.connection.CreateCommand();
        }

        DbCommand BuildCommand(IObjectQuery<T> query)
        {
            var command = this.CreateCommand();
            command.CommandText = query.CommandText;
            command.CommandType = query.CommandType;
            query.ParameterData.Fill<T>(command);
            return command;
        }

        #endregion

        #region Parameters

        #endregion


        public ObjectQuery<T> CreateQuery()
        {
            return new ObjectQuery<T>(this);
        }

        #region Execute

        internal IEnumerable<T> ExecuteInternal(string text, CommandType commandType, object values)
        {
            var query = this.CreateQuery();
            query.CommandText = text;
            query.CommandType = commandType;
            if (values != null)
            {
                foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(values))
                {
                    query.ParameterData.Add("@" + item.Name, item.GetValue(values));
                }
            }
            return query;
        }

        internal List<T> ExecuteInternal(IObjectQuery<T> query)
        {
            if (query.Provider == null)
                query.Provider = this;
            var command = this.BuildCommand(query);
            var reader = command.ExecuteReader();
            List<T> result;
            try
            {
                result = Emit.EmitReader.GetList<T>(reader);
                reader.Close();
                query.ParameterData.FillValue(command);
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
                command.Dispose();
            }
            return result;
        }


        internal int ExecuteNonQueryInternal(string text, CommandType commandType, object values)
        {
            using (var command = this.CreateCommand())
            {
                command.CommandText = text;
                command.CommandType = commandType;

                if (values != null)
                {
                    foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(values))
                    {
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@" + item.Name, item.GetValue(values)));
                    }
                }
                return command.ExecuteNonQuery();     
            }

        }


        internal int ExecuteNonQueryInternal(IObjectQuery<T> query)
        {
            if (query.Provider == null)
                query.Provider = this;
            var command = this.BuildCommand(query);
            int result;
            try
            {
                result = command.ExecuteNonQuery();
            }
            finally
            {
                command.Dispose();
            }
            return result;
        }

        public X ExecuteScalar<X>(IObjectQuery<T> query)
        {
            if (query.Provider == null)
                query.Provider = this;
            var command = this.BuildCommand(query);
            var result = (X)command.ExecuteScalar();
            query.ParameterData.FillValue(command);
            this.ReleaseConnection();
            return result;
        }

        #endregion

        CommandType CheckCommandType(string sql)
        {

            return sql.IndexOf("select ", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                sql.IndexOf("update ", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                sql.IndexOf("delete ", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                sql.IndexOf("insert ", StringComparison.CurrentCultureIgnoreCase) != -1 ? CommandType.Text : CommandType.StoredProcedure;
        }


        #region Execute

        public IEnumerable<T> Execute(string text, object values = null)
        {
            return ExecuteInternal(text, this.CheckCommandType(text), values);
        }

        public int ExecuteNonQuery(string text, object values = null)
        {
            return this.ExecuteNonQueryInternal(text, this.CheckCommandType(text), values);
        }




        
        /// <summary>
        /// 判断存储过程是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool ExistsStoredProcedure(string table)
        {
            var query = @"
                IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('+ @ObjectName +') AND type in ('P', 'PC'))
                    Set @Exists = 1
                Else
                    Set @Exists = 0";

            this.EnsureConnection();
            var command = this.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            var objectName = new System.Data.SqlClient.SqlParameter("@ObjectName", SqlDbType.NVarChar, 200);
            objectName.Value = table;
            var exists = new System.Data.SqlClient.SqlParameter("@Exists", SqlDbType.Bit);
            exists.Direction = ParameterDirection.Output;
            command.Parameters.Add(objectName);
            command.Parameters.Add(exists);

            command.ExecuteNonQuery();
            var result = (bool)command.Parameters["@Exists"].Value;
            this.ReleaseConnection();
            return result;
        }

        #endregion


        public int Count(IObjectQuery<T> query)
        {
            query.Builder.Count = true;
            return ExecuteScalar<int>(query);
        }


        #region CRUD

        public bool Insert(T value)
        {
            var query = this.CreateQuery();
            query.Builder.QueryType = QueryType.Insert;
            using (var command = this.BuildCommand(query))
            {
                return Emit.EmitInsert<T>.Fill(command, value) > 0;
            }
        }

        public bool Update(T value)
        {
            var query = this.CreateQuery();
            query.Builder.QueryType = QueryType.Update;
            using (var command = this.BuildCommand(query))
            {
                return Emit.EmitUpdate<T>.Fill(command, value) > 0;
            }
        }


        public bool Delete(T value)
        {
            var query = this.CreateQuery();
            query.Builder.QueryType = QueryType.Delete;
            using (var command = this.BuildCommand(query))
            {
                return Emit.EmitDelete<T>.Fill(command, value) > 0;
            }
        }

        #endregion


        #region Ext

        public virtual IEnumerable<T> SelectAll()
        {
            return this.CreateQuery();
        }


        #endregion





    }
}
