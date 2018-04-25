using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Epic.Data
{
    public abstract class DefaultSqlDataProvider : System.Configuration.Provider.ProviderBase
    {

        protected string _databaseOwner;
        protected string _connectionString;


        public string Encode(string value)
        {
            return value.Replace("'", String.Empty).Replace("\"", String.Empty);
        }

        /// <summary>
        /// 数据库所有者
        /// </summary>
        public string DatabaseOwner
        {
            get { return this._databaseOwner; }
        }

        /// <summary>
        /// 数据库连接字串
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>SqlConnection 对象</returns>
        protected virtual SqlConnection Open()
        {
            return new SqlConnection(this.ConnectionString);
        }


        #region Paging

        /// <summary>
        /// 数据库行数统计
        /// </summary>
        /// <param name="tables">目标表</param>
        /// <param name="filter">条件</param>
        /// <returns>结果行数</returns>
        public int RecordCount(string tables, string filter)
        {
            using (SqlConnection connection = this.Open())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = this._databaseOwner + ".Epic_CommonRecordCount";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Tables", SqlDbType.VarChar, 1500).Value = tables;
                command.Parameters.Add("@Filter", SqlDbType.NVarChar, 1000).Value = filter;
                command.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                int result = (int)command.Parameters["@RecordCount"].Value;
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 数据库行数统计
        /// </summary>
        /// <param name="paging">分页配置对象</param>
        public int RecordCount<T>(Paging<T> paging)
        {
            return paging.RecordCount = RecordCount(paging.Tables, paging.Where.Filter);
        }

        /// <summary>
        /// 数据库分页
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="param">分页配置对象</param>
        /// <returns>分页结果对象集合</returns>
        DataList<T> Paging<T>(Paging<T> param, ParameterList parameters) where T : Epic.Components.IComponent, new()
        {
            return this.ExecuteSPList<T>("Epic_CommonPaging", parameters);
        }

        /// <summary>
        /// 数据库分页(自动统计行数)
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="param">分页配置对象</param>
        /// <returns>分页结果对象集合</returns>
        public DataList<T> PagingWithCompact<T>(Paging<T> param) where T : Epic.Components.IComponent, new()
        {
            if (param.AbsolutePage < 1) param.AbsolutePage = 1;
            if (!param.IsSetRecordCount) RecordCount(param);

            DataList<T> result;
            ParameterList parameters = null;
            if (param.RecordCount > 0)
            {
                parameters = new ParameterList();
                parameters.Add("@AbsolutePage", SqlDbType.Int, ParameterDirection.InputOutput).Value = param.AbsolutePage;
                parameters.Add("@PageSize", SqlDbType.Int).Value = param.PageSize;
                parameters.Add("@PageCount", SqlDbType.Int, ParameterDirection.InputOutput).Value = param.PageCount;
                parameters.Add("@RecordCount", SqlDbType.Int, ParameterDirection.InputOutput).Value = param.RecordCount;
                parameters.Add("@Fields", SqlDbType.VarChar, 1500).Value = param.Fields;
                parameters.Add("@Tables", SqlDbType.VarChar, 1500).Value = param.Tables;
                parameters.Add("@Order", SqlDbType.VarChar, 200).Value = param.Order;
                parameters.Add("@Filter", SqlDbType.VarChar, 1000).Value = param.Where.Filter;
                result = Paging<T>(param, parameters);
            }
            else
                result = new DataList<T>();

            result.Paging = param;
            if (parameters != null)
            {
                param.AbsolutePage = (int)parameters.Result["@AbsolutePage"];
                param.PageCount = (int)parameters.Result["@PageCount"];
                param.RecordCount = (int)parameters.Result["@RecordCount"];
            }
            return result;
        }


        #endregion


        #region 基本方法

        T FillItem<T>(SqlDataReader dr) where T : Epic.Components.IComponent, new()
        {
            T result = new T();
            result.Parse(dr);
            return result;
        }

        #region ExecuteNonQuery

        protected int ExecuteTextNonQuery(string query, ParameterList parameters = null)
        {
            return ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        protected int ExecuteSPNonQuery(string query, ParameterList parameters = null)
        {
            return ExecuteNonQuery(query, CommandType.StoredProcedure, parameters);
        }

        int ExecuteNonQuery(string query, CommandType commandType, ParameterList parameters = null)
        {
            using (SqlConnection connection = this.Open())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = commandType;

                command.Parameters.AddRange(parameters);
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (parameters != null)
                    parameters.BuildResult();

                connection.Close();
                return result;
            }
        }


        #endregion

        #region ExecuteSingle

        protected T ExecuteTextScalar<T>(string query, ParameterList parameters = null)
        {
            return ExecuteScalar<T>(query, CommandType.Text, parameters);
        }

        protected T ExecuteSPScalar<T>(string query, ParameterList parameters = null)
        {
            return ExecuteScalar<T>(query, CommandType.StoredProcedure, parameters);
        }

        T ExecuteScalar<T>(string query, CommandType commandType, ParameterList parameters = null)
        {
            using (SqlConnection connection = this.Open())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = commandType;

                command.Parameters.AddRange(parameters);
                connection.Open();

                T result = (T)command.ExecuteScalar();
     
                if (parameters != null)
                    parameters.BuildResult();

                connection.Close();
                return result;
            }
        }

        #endregion

        #region ExecuteSingle

        protected T ExecuteTextSingle<T>(string query, ParameterList parameters = null) where T : Epic.Components.IComponent, new()
        {
            return ExecuteSingle<T>(query, CommandType.Text, parameters);
        }

        protected T ExecuteSPSingle<T>(string query, ParameterList parameters = null) where T : Epic.Components.IComponent, new()
        {
            return ExecuteSingle<T>(query, CommandType.StoredProcedure, parameters);
        }



        T ExecuteSingle<T>(string query, CommandType commandType, ParameterList parameters = null) where T : Epic.Components.IComponent, new()
        {
            using (SqlConnection connection = this.Open())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                T result;
                if (dr.Read())
                    result = FillItem<T>(dr);
                else
                    result = default(T);

                dr.Close();
                if (parameters != null)
                    parameters.BuildResult();

                connection.Close();
                return result;
            }
        }

        #endregion

        #region ExecuteList

        /// <summary>
        /// Sql 语句, 批量记录 多数据集返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected DataList<T> ExecuteSPList<T>(string query, ParameterList parameters = null) where T : Epic.Components.IComponent, new()
        {
            return ExecuteList<T>(query, CommandType.StoredProcedure, parameters);
        }

        /// <summary>
        /// 存储过程, 批量记录 多数据集返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected DataList<T> ExecuteTextList<T>(string query, ParameterList parameters = null) where T : Epic.Components.IComponent, new()
        {
            return ExecuteList<T>(query, CommandType.Text, parameters);
        }

        DataList<T> ExecuteList<T>(string query, CommandType commandType, ParameterList parameters = null) where T : Epic.Components.IComponent, new()
        {
            using (SqlConnection connection = this.Open())
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandType = commandType;

                command.Parameters.AddRange(parameters);

                DataList<T> result = new DataList<T>();
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                do
                {
                    while (dr.Read())
                    {
                        result.Add(FillItem<T>(dr));
                    }
                } while (dr.NextResult());
  
                dr.Close();

                if (parameters != null)
                    parameters.BuildResult();

                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 根据格式化字符串 获取列表
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="query">查询: 例: Select * From [Test] Where ID = {0} And Name = '{1}'</param>
        /// <param name="args">参数: 例: 1, "Jack"</param>
        /// <returns></returns>
        protected List<T> ExecuteListByFormat<T>(string query, params object[] args) where T : Epic.Components.IComponent, new()
        {
            if (String.IsNullOrWhiteSpace(query)) return null;
            if (args != null && args.Length > 0)
                query = String.Format(query, args);

            return this.ExecuteTextList<T>(query);
        }

        /// <summary>
        /// 根据参数 获取列表
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="query">查询: 例: Select * From [Test] Where ID = @p0 And Name = @p1</param>
        /// <param name="args">参数: 例: 1, "Jack"</param>
        /// <returns></returns>
        protected List<T> ExecuteListByParameters<T>(string query, params object[] args) where T : Epic.Components.IComponent, new()
        {
            if (String.IsNullOrWhiteSpace(query)) return null;
            ParameterList parameters = null;
            if (args != null && args.Length > 0)
            {
                parameters = new ParameterList();
                for (int i = 0; i < args.Length; i++)
                {
                    parameters.Add("@p" + i, args[0]);
                }
            }

            return this.ExecuteTextList<T>(query, parameters);
        }


        #endregion



        protected int ExecuteNonQuery(string query, params object[] args)
        {
            if (args == null || args.Length == 0) return -1;
            var sb = new StringBuilder();
            IEnumerable e;
            foreach (var item in args)
            {
                e = item as IEnumerable;
                if (e != null)
                {
                    foreach (var i in e)
                    {
                        sb.AppendFormat(query, i);
                    }
                }
                else
                    sb.AppendFormat(query, item);
            }
            return this.ExecuteTextNonQuery(sb.ToString());
        }


        public List<T> ExecuteBatchList<T>(string query, int[] ids) where T : Epic.Components.IComponent, new()
        {
            return ExecuteBatchList<T, int>(query, ids);
        }


        public List<T> ExecuteBatchList<T, K>(string query, K[] ids) where T : Epic.Components.IComponent, new()
        {
            if (ids == null || ids.Length == 0) return new List<T>();

            var sb = new StringBuilder();
            foreach (var item in ids.Distinct())
            {
                sb.AppendFormat(query, item);
            }
            return this.ExecuteList<T>(sb.ToString(), CommandType.Text);
        }

        #endregion
    }
}
