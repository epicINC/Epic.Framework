using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    /// <summary>
    /// RSS 附件
    /// </summary>
    public class RSSEnclosure
    {
        /// <summary>
        /// 附件地址
        /// </summary>
        public string url
        {
            get;
            set;
        }

        /// <summary>
        /// 附件长度 单位 byte
        /// </summary>
        public string length
        {
            get;
            set;
        }

        /// <summary>
        /// MIME 类型
        /// </summary>
        public string type
        {
            get;
            set;
        }
    }
}
