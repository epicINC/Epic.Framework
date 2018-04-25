using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace Epic.Data.Mongo
{
    /// <summary>
    /// Mongo 自定义扩展
    /// </summary>
    public static class MongoExtensions
    {
        public static T FindAndModify<T>(this MongoCollection<T> value, string where, string update)
        {
            var result = value.FindAndModify(ToQuery(where), SortBy.Null, ToUpdate(update));
            return result.GetModifiedDocumentAs<T>();
        }

        public static T FindOne<T>(this MongoCollection<T> value, string where)
        {
            return value.FindOne(ToQuery(where));
        }

        static IMongoQuery ToQuery(string value)
        {
            return Query.Where(BsonJavaScript.Create(value));
        }

        static IMongoUpdate ToUpdate(string value)
        {
            return new UpdateDocument("$set", UpdateDocument.Create(value));
        }

    }
}
