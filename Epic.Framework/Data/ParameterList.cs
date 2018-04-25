using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Epic.Data
{
    /// <summary>
    /// 参数
    /// </summary>
    public class ParameterList : List<SqlParameter>
    {


        #region Add

        public new SqlParameter Add(SqlParameter value)
        {
            base.Add(value);
            return value;
        }

        SqlParameter Add(SqlParameter value, ParameterDirection direction)
        {
            value.Direction = direction;
            this.Add(value);
            return value;
        }

        public SqlParameter Add(string parameterName, object value)
        {
            return this.Add(new SqlParameter(parameterName, value));
        }

        public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
        {
            return this.Add(new SqlParameter(parameterName, sqlDbType));
        }

        public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
        {
            return this.Add(new SqlParameter(parameterName, sqlDbType, size));
        }

        public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
        {
            return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
        }

        public SqlParameter AddWithValue(string parameterName, object value)
        {
            return this.Add(new SqlParameter(parameterName, value));
        }

        public SqlParameter Add(string parameterName, SqlDbType sqlDbType, ParameterDirection direction)
        {
            return this.Add(new SqlParameter(parameterName, sqlDbType), direction);
        }

        public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, ParameterDirection direction)
        {
            return this.Add(new SqlParameter(parameterName, sqlDbType, size), direction);
        }

        public void AddRange(SqlParameter[] values)
        {
            this.AddRange(values);
        }


        #endregion


        Dictionary<string, object> result;

        public Dictionary<string, object> Result
        {
            get
            {
                return this.result;
            }
        }

        public Dictionary<string, object> BuildResult()
        {
            this.result = new Dictionary<string, object>();

            foreach (var item in this)
            {
                if (item.Direction == ParameterDirection.Input)
                    continue;
                result.Add(item.ParameterName, item.Value);

            }
            return result;
        }










    }
}
