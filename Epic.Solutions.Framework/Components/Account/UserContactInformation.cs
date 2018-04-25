using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 用户联系信息
    /// </summary>
    public class UserContactInformation
    {

        /// <summary>
        /// 手机
        /// </summary>
        public UserPhoneInformation Mobile
        {
            get;
            set;
        }

        /// <summary>
        /// 其他电话
        /// </summary>
        public List<UserPhoneInformation> Phones
        {
            get;
            set;
        }

        /// <summary>
        /// 即时通讯工具
        /// </summary>
        public List<UserIMInformation> IMS
        {
            get;
            set;
        }

        /// <summary>
        /// 地址信息
        /// </summary>
        public UserLocationInformation Address
        {
            get;
            set;
        }

        /// <summary>
        /// 个人网站
        /// </summary>
        public string Website
        {
            get;
            set;
        }



    }
}
