using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    /// <summary>
    /// 图片
    /// 参看 http://www.rssboard.org/rss-specification#ltimagegtSubelementOfLtchannelgt
    /// </summary>
    public class RSSImage
    {
        // 必填元素 Required

        /// <summary>
        /// 图片地址 Required
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string url
        {
            get;
            set;
        }

        /// <summary>
        /// 图标标题 Required
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 图片地址 Required
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string link
        {
            get;
            set;
        }

        // 可选元素 Optional

        /// <summary>
        /// 宽度 Optional
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string width
        {
            get;
            set;
        }

        /// <summary>
        /// 高度 Optional
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string height
        {
            get;
            set;
        }

        /// <summary>
        /// 描述 Optional
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description
        {
            get;
            set;
        }
    }
}
