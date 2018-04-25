using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Epic.Extensions
{
    public static class SqlParameterExtensions
    {
        public static void Add(this List<SqlParameter> collection, string name, SqlDbType dbType, object value)
        {
            var item = new SqlParameter(name, dbType);
            item.Value = value;
            collection.Add(item);
        }

        public static void Add(this List<SqlParameter> collection, string name, SqlDbType dbType, int size, object value)
        {
            var item = new SqlParameter(name, dbType, size);
            item.Value = value;
            collection.Add(item);
        }
    }
}
