using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Epic.Data.Mongo.Samus.FluentAPI
{
    public class MongoAPIConfig<T>
    {
        public MongoAPIConfig()
        {
            this.Type = typeof(T);
            this.Name = this.Type.Name;
            this.FullName = this.Type.FullName;
            this.ID = QueryObjectMethod.Property<T, int>(this.Type, "ID");
        }

        internal Type Type
        {
            get;
            set;
        }

        /// <summary>
        /// 表别名
        /// </summary>
        internal string TableAlias
        {
            get;
            set;
        }

        internal string Name
        {
            get;
            set;
        }

        internal string FullName
        {
            get;
            set;
        }


        internal string Alias
        {
            get;
            set;
        }


        internal PropertyCache<T, int> ID
        {
            get;
            set;
        }

    }
}
