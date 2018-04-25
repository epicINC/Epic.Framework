using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Epic.Components
{

    /// <summary>
    /// 登录凭据类型
    /// </summary>
    public enum AccountCredentialType
    {

        /// <summary>
        /// 账号登录
        /// </summary>
        [Description("账号登录")]
        ID = 1 << 0,

        /// <summary>
        /// 账号登录
        /// </summary>
        [Description("账号登录")]
        Account = 1 << 1,

        /// <summary>
        /// CA登录
        /// </summary>
        [Description("CA登录")]
        CA = 1 << 2,


        /// <summary>
        /// 邮箱登录
        /// </summary>
        [Description("邮箱登录")]
        Email = 1 << 3,
    }

}
