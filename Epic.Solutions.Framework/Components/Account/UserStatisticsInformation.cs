using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    /// <summary>
    /// 用户统计信息
    /// </summary>
    public class UserStatisticsInformation
    {
        /// <summary>
        /// 最后活动时间
        /// </summary>
        public DateTime Activity
        {
            get;
            set;
        }

        /// <summary>
        /// 登陆次数
        /// </summary>
        public int LoginCount
        {
            get;
            set;
        }


    }
}
