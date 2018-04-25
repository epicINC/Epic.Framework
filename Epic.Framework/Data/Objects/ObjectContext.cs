using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Epic.Data.Objects;
using System.Data.Common;
using Epic.Components;

namespace Epic.Data.Objects
{

    public class ObjectContext
    {
        string connectionString;
        DbConnection connection;

#if DEBUG
        public ObjectContext()
            : this("Password=s123;Persist Security Info=True;User ID=sa;Initial Catalog=DB_BetaUN;Data Source=(local)")
        // public ObjectContext() : this("Password=w3ij3dMEgrZE;Persist Security Info=True;User ID=FrontUser;Initial Catalog=UsashopFront;Data Source=192.168.1.111")
        {
        }
#endif
        public ObjectContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal DbConnection Connection
        {
            get { return this.connection; }
        }


        public ObjectSet<T> CreateObjectSet<T>()
        {
            return new ObjectSet<T>(this);
        }





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



        ObjectQueryProvider queryProvider;
        protected internal IQueryProvider QueryProvider
        {
            get
            {
                if (this.queryProvider == null)
                    this.queryProvider = new ObjectQueryProvider(this);
                return this.queryProvider;
            }
        }



        //internal protected T SelectByID(int id)
        //{
        //    ParameterList parameters = new ParameterList();
        //    parameters.AddWithValue("@ID", id);
        //    return this.ExecuteSPSingle<T>(TableSchema<T>.SPUpdateName, parameters);

        //}
    }
}
