using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Epic.Components
{

    /// <summary>
    /// 登陆信息
    /// </summary>
    public interface IAccount
    {

        /// <summary>
        /// 账号
        /// </summary>
        string Account
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 随机字串
        /// </summary>
        string Salt
        {
            get;
            set;
        }

        /// <summary>
        /// CA
        /// </summary>
        string CA
        {
            get;
            set;
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 登陆凭据类型
        /// </summary>
        AccountCredentialType CredentialType
        {
            get;
            set;
        }

        /// <summary>
        /// 密码加密类型
        /// </summary>
        AccountPasswordEncryptType PasswordEncryptType
        {
            get;
            set;
        }
    }
}
