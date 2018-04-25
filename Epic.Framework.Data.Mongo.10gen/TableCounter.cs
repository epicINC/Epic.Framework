using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Data.Mongo.Gen10
{
    /// <summary>
    /// 自增长 实现类
    /// </summary>
    internal class TableCounter
    {
        [BsonId]
        public ObjectId _id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

    }

}
