using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using System.Linq.Expressions;
using Epic.Data.Mongo.Samus.FluentAPI;
using System.Data;
using Epic.Extensions;

namespace Epic.Data.Mongo.Samus
{
    public class SamusDataProvider<T> : Epic.Data.Mongo.MongoDataProviderBase<T>, Epic.Data.Mongo.IMongoDataProvider<T> where T : class
    {

        SamusConnection connection;
        TableCounterManager<T> counterManager;

        TableCounterManager<T> CounterManager
        {
            get { return this.counterManager ?? (this.counterManager = new TableCounterManager<T>(this.Connection)); }
        }


        IDbConnection IMongoDataProvider<T>.Connection
        {
            get { return this.Connection; }
        }

        public SamusConnection Connection
        {
            get { return this.connection ?? (this.connection = new SamusConnection(this.connectionString)); }
        }

        internal IMongoCollection<T> Table
        {
            get { return this.Connection.GetDatabase().GetCollection<T>(MongoAPI<T>.FullName); }
        }


        public override T Find(object selector)
        {
            return this.Table.FindOne(selector);
        }

        public override T Find(string javascriptWhere)
        {
            return this.Table.FindOne(javascriptWhere);
        }

        public override T Find(Expression<Func<T, bool>> selector)
        {
            return this.Table.FindOne<T>(selector);
        }

        public override IQueryable<T> FindAll(Expression<Func<T, bool>> selector = null)
        {
            if (selector == null) return this.Table.Linq<T>();
            return this.Table.Linq().Where(selector);
        }

        public override void Insert(T value)
        {
            MongoAPI<T>.SetID(value, this.CounterManager.Counter);
            this.Table.Insert(value);
            this.Connection.Close();
        }


        public override void Save(T value)
        {
            if (MongoAPI<T>.IsValidID(value))
            this.Table.Update(value, new Document("ID", MongoAPI<T>.GetID(value)));
            else
                this.Insert(value);
        }

        public override void Delete(T value)
        {
            this.Table.Remove(value);
        }

        public override void Delete(Expression<Func<T, bool>> selector)
        {
            this.Table.Remove(selector);
        }

        public override void Clear()
        {
            this.Table.Remove(new Document());
            this.CounterManager.Reset();
        }
    }

}
