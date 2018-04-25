using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epic.Data.Mongo.Gen10.FluentAPI;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Linq.Expressions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Epic.Extensions;
using System.Data.Common;
using System.Data;
using Epic.Extensions;

namespace Epic.Data.Mongo.Gen10
{
    public class Gen10Connection : DbConnection
    {

        public Gen10Connection(string connectionString)
        {
            InitConnectionString(connectionString);
        }

        void InitConnectionString(string connectionString)
        {
            Errors.CheckArgumentNull(connectionString, "connectionString").Throw();

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
        MongoServer connection;
        string database;
        string dataSource;

        public override void Open()
        {
            if (!this.isOpen)
            {
                if (this.connection == null)
                {
                    var client = new MongoClient(this.ConnectionString);
                    this.connection = client.GetServer();
                }
                connection.Connect();
            }
        }





        public override void Close()
        {
            if (!this.isOpen) return;
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

        public MongoDatabase GetDatabase()
        {
            return this.GetDatabase(this.database);
        }

        public MongoDatabase GetDatabase(string name)
        {
            this.Open();
            return connection.GetDatabase(name);
        }

        public IEnumerable<MongoDatabase> GetDatabases(string names)
        {
            var result = new List<MongoDatabase>();
            this.Open();
            return names.Split(',').Select(e => this.connection.GetDatabase(e));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (connection != null)
                connection.Disconnect();
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
