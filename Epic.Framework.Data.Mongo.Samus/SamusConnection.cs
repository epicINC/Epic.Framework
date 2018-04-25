using MongoDB;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using System.Data;

namespace Epic.Data.Mongo.Samus
{
    public class SamusConnection : DbConnection
    {

        internal SamusConnection(string connectionString)
        {
            InitConnectionString(connectionString);
        }

        /// <summary>
        /// 初始化 connectionString
        /// </summary>
        /// <param name="connectionString"></param>
        void InitConnectionString(string connectionString)
        {
            object database, servers;
            var builder = new DbConnectionStringBuilder();
            builder.ConnectionString = connectionString;
            builder.TryGetValue("database", out database);
            builder.TryGetValue("servers", out servers);

            this.database = database.ToString();
            this.dataSource = connectionString;
            this.ConnectionString = servers.ToString();
        }


        bool isOpen;
        MongoDB.Mongo connection;
        string database;
        string dataSource;

        public override void Open()
        {
            if (this.isOpen) return;
            if (this.connection == null)
            {
                this.connection = new MongoDB.Mongo(this.ConnectionString);
                
            }
            this.connection.Connect();
            
        }

        public override void Close()
        {
            if (this.isOpen) return;
            connection.Disconnect();
            this.isOpen = false;
        }

        public override string Database
        {
            get { return this.database; }
        }

        public override void ChangeDatabase(string databaseName)
        {
            this.database = databaseName;
        }

        public IMongoDatabase GetDatabase()
        {
            return this.GetDatabase(this.database);
        }

        public IMongoDatabase GetDatabase(string name)
        {
            this.Open();
            return connection.GetDatabase(name);
        }

        public IEnumerable<IMongoDatabase> GetDatabases()
        {
            this.Open();
            return connection.GetDatabases();
        }

        public IEnumerable<IMongoDatabase> GetDatabases(string names)
        {
            return this.GetDatabases().Intersect(names.Split(','), (e, k) => e.Name == k);
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (connection != null)
            {
                connection.Disconnect();
                connection.Dispose();
            }
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override string ConnectionString
        {
            get;
            set;
        }

        protected override DbCommand CreateDbCommand()
        {
            throw new NotImplementedException();
        }

        public override string DataSource
        {
            get { return this.dataSource; }
        }

        public override string ServerVersion
        {
            get { throw new NotImplementedException(); }
        }

        public override ConnectionState State
        {
            get { throw new NotImplementedException(); }
        }
    }
}
