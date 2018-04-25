using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Epic.Data.Mongo
{
    public interface IMongoDataProvider<T> where T : class
    {
        IDbConnection Connection { get; }

        bool Exists(Expression<Func<T, bool>> selector);

        T Find(Expression<Func<T, bool>> selector);
        T Find(object selector);
        T Find(string javascriptWhere);

        IQueryable<T> FindAll(Expression<Func<T, bool>> selector = null);

        void Insert(IEnumerable<T> collection);
        void Insert(T value);

        void Save(IEnumerable<T> collection);
        void Save(T value);

        void Delete(T value);
        void Delete(Expression<Func<T, bool>> selector);
        void Delete(IEnumerable<T> collection);

        void Clear();
    }
}
