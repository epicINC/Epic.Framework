using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 用户电话信息
    /// </summary>
    public class UserPhoneInformation
    {
        /// <summary>
        /// 类型, 比如 工作 家庭等
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 国家区号
        /// </summary>
        public string Country
        {
            get;
            set;
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone
        {
            get;
            set;
        }


    }
}
