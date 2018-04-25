using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Epic.Extensions;
using System.Collections.Specialized;

namespace Epic.Data.Mongo
{
    /// <summary>
    /// MongoDB 抽象 DataProvider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MongoDataProviderBase<T> : Epic.Data.DataProviderBase where T : class
    {

        protected string connectionString;

        public override void Initialize(string name, NameValueCollection config)
        {
            //base.Initialize(name, config);
            if (this.connectionString == null)
                this.connectionString = GetConnectionString(config["connectionStringName"]);
        }


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<T, bool>> selector)
        {
            return this.Find(selector) != null;
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public abstract T Find(object selector);

        public abstract T Find(string javascriptWhere);

        public abstract T Find(Expression<Func<T, bool>> selector);


        public abstract IQueryable<T> FindAll(Expression<Func<T, bool>> selector = null);



        public abstract void Insert(T value);

        public virtual void Insert(IEnumerable<T> collection)
        {
            collection.ForEach(e => this.Insert(e));
        }


        public abstract void Save(T value);

        public virtual void Save(IEnumerable<T> collection)
        {
            collection.ForEach(e => this.Save(e));
        }


        public abstract void Delete(T value);

        public abstract void Delete(Expression<Func<T, bool>> selector);

        public virtual void Delete(IEnumerable<T> collection)
        {
            collection.ForEach(e => this.Delete(e));
        }

        public abstract void Clear();

    }

}
