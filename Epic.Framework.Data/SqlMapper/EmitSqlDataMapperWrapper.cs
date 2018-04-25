using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace Epic.Data.Mapper
{
    internal static class EmitSqlDataMapperWrapper<T> where T : class
    {
        static DataMapper<DbDataReader, T> mapper;
        static EmitSqlDataMapperWrapper()
        {
            mapper = new EmitSqlDataMapper<T>();
        }

        public static int Insert(SqlCommand command, T value)
        {

            return mapper.Insert(command, value);
        }


        public static int Update(SqlCommand command, T value)
        {
            return mapper.Update(command, value);
        }


        public static int Delete(SqlCommand command, T value)
        {
            return mapper.Delete(command, value);
        }

        public static T Convert(DbDataReader reader)
        {
            return mapper.Convert(reader);
        }
    }
}
