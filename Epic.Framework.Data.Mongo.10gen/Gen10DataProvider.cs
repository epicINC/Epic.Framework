using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;
using Epic.Extensions;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Epic.Components;
using MongoDB.Driver;
using Epic.Data.Mongo.Gen10.FluentAPI;
using System.Data;

namespace Epic.Data.Mongo.Gen10
{
    public class Gen10DataProvider<T> : Epic.Data.Mongo.MongoDataProviderBase<T>, Epic.Data.Mongo.IMongoDataProvider<T> where T : class
    {



        static Gen10DataProvider<T> provider;
        internal const string ProviderName = "MongoDataProvider";


        public static Gen10DataProvider<T> Current
        {
            get
            {
                if (provider == null)
                {
                    provider = new Gen10DataProvider<T>();
                    provider.Initialize(Gen10DataProvider<T>.ProviderName);
                }
                return provider;
            }
        }


        Gen10Connection connection;
        TableCounterManager<T> counterManager;

        TableCounterManager<T> CounterManager
        {
            get { return this.counterManager ?? (this.counterManager = new TableCounterManager<T>(this.Connection)); }
        }


        IDbConnection IMongoDataProvider<T>.Connection
        {
            get { return this.Connection; }
        }

        public Gen10Connection Connection
        {
            get
            {
                if (this.connection == null)
                    this.connection = new Gen10Connection(this.connectionString);
                return this.connection;
            }
        }

        internal MongoCollection<T> Table
        {
            get { return this.Connection.GetDatabase().GetCollection<T>(MongoAPI<T>.FullName); }
        }

        public override T Find(object selector)
        {
            return default(T);
        }


        public override T Find(string javascriptWhere)
        {
            return this.Table.FindOne(javascriptWhere);
        }

        public T Find<K>(Expression<Func<T, K>> selector, K value)
        {
            var query = Query<T>.EQ(selector, value);
            
            return this.Table.FindOne(query);
        }


        public override T Find(Expression<Func<T, bool>> selector)
        {
            return this.Table.AsQueryable().SingleOrDefault(selector);
        }

        public override IQueryable<T> FindAll(Expression<Func<T, bool>> selector = null)
        {
            if (selector == null)
                return this.Table.FindAll().AsQueryable();

            return this.Table.AsQueryable().Where(selector);
        }



        public override void Insert(T value)
        {
            //MongoAPI<T>.SetID(value, this.CounterManager.Counter);
            this.Table.Insert(value);
        }


        public override void Save(T value)
        {
            //if (MongoAPI<T>.HasObjectID(value))
            //    this.Table.Save(value);
            //else
            //    this.Insert(value);
        }

        public override void Delete(T value)
        {
            //this.Table.Remove(Query.EQ("_id", MongoAPI<T>.GetObjectID(value)));
        }

        public override void Delete(Expression<Func<T, bool>> expression)
        {
            this.Table.Remove(Query<T>.Where(expression));
        }

        public override void Clear()
        {
            this.Table.RemoveAll();
            this.CounterManager.Reset();
        }




        /*
        public IEnumerable<T> PagingWithCompact(OffsetPaging param)
        {
            return this.PagingWithCompact((dynamic)param);
        }

        public IEnumerable<T> PagingWithCompact(MongoOffsetPaging param)
        {
            var result = param.HasFilter ? this.Connection.Table<T>().Find(param.MongoFilter) : this.Connection.Table<T>().FindAll();

            param.RecordCount = result.Count();

            if (param.HasOrder)
            {
                SortByDocument sort = new SortByDocument();

                foreach (var item in param.Order)
                {
                    switch (item.Value)
                    {
                        case SortDirection.Asc:
                            sort.Add(item.Key, 1);
                            break;
                        case SortDirection.Desc:
                            sort.Add(item.Key, -1);
                            break;
                        default:
                            sort.Add(item.Key, -1);
                            break;
                    }

                }
                return result.SetSortOrder(sort).SetLimit(param.PageSize).SetSkip(param.OffSet);
            }
            return result.SetLimit(param.PageSize).SetSkip(param.OffSet);
        }
         */

    }
}
