using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Epic.Extensions
{
    /// <summary>
    /// 数据层扩展方法
    /// </summary>
    public static class SqlClientExtensions
    {
        /// <summary>
        /// 创建 Command 对象, 默认 CommandType StoredProcedure
        /// </summary>
        /// <param name="value"></param>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlCommand CreateCommand(this SqlConnection value, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var result = value.CreateCommand();
            result.CommandText = commandText;
            result.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
                result.Parameters.AddRange(parameters);
            return result;
        }

        public static SqlCommand CreateCommand(this SqlConnection value, string commandText, CommandType commandType, List<SqlParameter> parameters)
        {
            return parameters == null || parameters.Count == 0 ? value.CreateCommand(commandText, commandType) : value.CreateCommand(commandText, commandType, parameters.ToArray());
        }

        public static SqlCommand CreateCommand(this SqlConnection value, string commandText, CommandType commandType)
        {
            var result = value.CreateCommand();
            result.CommandText = commandText;
            result.CommandType = commandType;
            return result;
        }


    }
}
