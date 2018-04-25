using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 用户即时通讯工具
    /// </summary>
    public class UserIMInformation
    {
        /// <summary>
        /// 服务商
        /// </summary>
        public string Server
        {
            get;
            set;
        }

        /// <summary>
        /// 通讯工具昵称或账号
        /// </summary>
        public string SN
        {
            get;
            set;
        }
    }
}
