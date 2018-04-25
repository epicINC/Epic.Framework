using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Epic.Extensions;

namespace Epic.Data.SqlServer
{
    /// <summary>
    /// 数据层抽象类
    /// </summary>
    public class DataProviderBase
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get;
            protected set;
        }

        #region Connection 连接对象

        /// <summary>
        /// 创建连接对象
        /// </summary>
        public SqlConnection Connection
        {
            get { return new SqlConnection(this.ConnectionString); }
        }

        /// <summary>
        /// 创建一个自动打开的连接对象
        /// </summary>
        public SqlConnection AutoConnection
        {
            get
            {
                var result = this.Connection;
                result.Open();
                return result;
            }
        }

        public bool TryConnection(bool autoThrow = false)
        {
            SqlConnection result = null;
            try
            {
                result = this.AutoConnection;
            }
            catch (SqlException e)
            {
                Loggin.Error(e);
                if (autoThrow)
                    throw;
            }
            finally
            {
                if (result != null)
                {
                    result.Close();
                    result.Dispose();
                }
            }
            return result != null;
        }

        #endregion


        #region ExecuteNonQuery

        public int ExecuteNonQuery(string commandText, CommandType type, params SqlParameter[] parameters)
        {
            using (var connection = this.AutoConnection)
            {
                return connection.CreateCommand(commandText, type, parameters).ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType type, List<SqlParameter> parameters)
        {
            return parameters == null || parameters.Count == 0 ? this.ExecuteNonQuery(commandText, type) : this.ExecuteNonQuery(commandText, type, parameters.ToArray());
        }

        public int ExecuteNonQuery(string commandText, CommandType type)
        {
            using (var connection = this.AutoConnection)
            {
                return connection.CreateCommand(commandText, type).ExecuteNonQuery();
            }
        }


        #endregion

        #region ExecuteText

        public int ExecuteTextNonQuery(string commandText, params SqlParameter[] parameters)
        {
            return this.ExecuteNonQuery(commandText, CommandType.Text, parameters);
        }

        public int ExecuteTextNonQuery(string commandText, List<SqlParameter> parameters)
        {
            return this.ExecuteNonQuery(commandText, CommandType.Text, parameters);
        }

        public int ExecuteTextNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(commandText, CommandType.Text);
        }

        #endregion

        #region ExecuteSP

        public int ExecuteSPNonQuery(string commandText, params SqlParameter[] parameters)
        {
            return this.ExecuteNonQuery(commandText, CommandType.StoredProcedure, parameters);
        }

        public int ExecuteSPNonQuery(string commandText, List<SqlParameter> parameters)
        {
            return this.ExecuteNonQuery(commandText, CommandType.StoredProcedure, parameters);
        }

        public int ExecuteSPNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(commandText, CommandType.StoredProcedure);
        }

        #endregion

        #region ExecuteSPReader<T>

        T ExecuteSPReader<T>(string commandText, List<SqlParameter> parameters, Func<DbDataReader, T> action)
        {
            using (var connection = this.AutoConnection)
            {
                using (var dr = connection.CreateCommand(commandText, CommandType.StoredProcedure, parameters).ExecuteReader())
                {
                    return action(dr);
                }
            }
        }

        T ExecuteSPReader<T>(string commandText, Func<DbDataReader, T> action, params SqlParameter[] parameters)
        {
            using (var connection = this.AutoConnection)
            {
                using (var dr = connection.CreateCommand(commandText, CommandType.StoredProcedure, parameters).ExecuteReader())
                {
                    return action(dr);
                }
            }
        }

        T ExecuteSPReader<T>(string commandText, Func<DbDataReader, T> action)
        {
            return this.ExecuteSPReader<T>(commandText, null, action);
        }

        #endregion

        #region ExecuteSPResult

        void ExecuteSPReader(string commandText, List<SqlParameter> parameters, Action<DbDataReader> action)
        {
            using (var connection = this.AutoConnection)
            {
                using (var dr = connection.CreateCommand(commandText, CommandType.StoredProcedure, parameters).ExecuteReader())
                {
                    action(dr);
                }
            }
        }

        void ExecuteSPReader(string commandText, Action<DbDataReader> action, params SqlParameter[] parameters)
        {
            using (var connection = this.AutoConnection)
            {
                using (var dr = connection.CreateCommand(commandText, CommandType.StoredProcedure, parameters).ExecuteReader())
                {
                    action(dr);
                }
            }
        }

        void ExecuteSPReader(string commandText, Action<DbDataReader> action)
        {
            this.ExecuteSPReader(commandText, null, action);
        }

        #endregion

        #region ExecuteSPSingle<T>

        /// <summary>
        /// 读取单个记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public T ExecuteSPSingle<T>(string commandText, List<SqlParameter> parameters, Converter<DbDataReader, T> converter)
        {
            return this.ExecuteSPReader<T>(commandText, parameters, e => e.Read() ? converter(e) : default(T));
        }

        public T ExecuteSPSingle<T>(string commandText, Converter<DbDataReader, T> converter, params SqlParameter[] parameters)
        {
            return this.ExecuteSPReader<T>(commandText, e => e.Read() ? converter(e) : default(T), parameters);
        }

        public T ExecuteSPSingle<T>(string commandText, Converter<DbDataReader, T> converter)
        {
            return this.ExecuteSPSingle<T>(commandText, null, converter);
        }

        #endregion

        #region ExecuteSPList<T>

        /// <summary>
        /// 读取批量记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public List<T> ExecuteSPList<T>(string commandText, List<SqlParameter> parameters, Converter<DbDataReader, T> converter)
        {
            return this.ExecuteSPReader<List<T>>(commandText, parameters, e =>
            {
                var result = new List<T>();
                do
                {
                    while (e.Read())
                    {
                        result.Add(converter(e));
                    }
                } while (e.NextResult());
                return result;
            });
        }

        public List<T> ExecuteSPList<T>(string commandText, Converter<DbDataReader, T> converter, params SqlParameter[] parameters)
        {
            return this.ExecuteSPReader<List<T>>(commandText, e =>
            {
                var result = new List<T>();
                do
                {
                    while (e.Read())
                    {
                        result.Add(converter(e));
                    }
                } while (e.NextResult());
                return result;
            }, parameters);
        }

        public List<T> ExecuteSPList<T>(string commandText, Converter<DbDataReader, T> converter)
        {
            return this.ExecuteSPList(commandText, null, converter);
        }

        #endregion

        #region ExecuteSPManual

        /// <summary>
        /// 读取记录 手工指定
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="action"></param>
        public void ExecuteSPManual(string commandText, List<SqlParameter> parameters, Action<DbDataReader> action)
        {
            this.ExecuteSPReader(commandText, parameters, action);
        }

        public void ExecuteSPManual(string commandText, Action<DbDataReader> action, params SqlParameter[] parameters)
        {
            this.ExecuteSPReader(commandText, action, parameters);
        }

        public void ExecuteSPManual(string commandText, Action<DbDataReader> action)
        {
            this.ExecuteSPManual(commandText, null, action);
        }

        #endregion
    }
}
