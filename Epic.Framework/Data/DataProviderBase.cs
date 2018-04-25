using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Epic.Data
{
    /// <summary>
    /// 数据 Provider 抽象类
    /// 提供了数据层 Provider 一些基本方法
    /// </summary>
    public abstract class DataProviderBase : System.Configuration.Provider.ProviderBase
    {

        protected void Initialize(string key)
        {
            this.Initialize(Epic.Configuration.EpicSettings.Current.Providers[key]);
        }

        protected void Initialize(ProviderSettings setting)
        {
            if (setting == null) return;
            this.Initialize(setting.Name, setting.Parameters);
        }


        static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name] ?? String.Empty;
        }


        /// <summary>
        /// 默认返回 索引第一条 连接字串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[0].ConnectionString;
        }


        public static string GetConnectionString(string connectionStringKey)
        {
            return ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionStringKey] ?? connectionStringKey].ConnectionString;
        }

        public static string GetDatabaseOwner(string databaseOwnerKey)
        {
            return GetAppSetting(databaseOwnerKey);
        }

 

 


    }
}
