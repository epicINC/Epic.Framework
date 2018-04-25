using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    /// <summary>
    /// RSS 输入框
    /// 参看 http://www.rssboard.org/rss-specification#lttextinputgtSubelementOfLtchannelgt
    /// </summary>
    public class RSSTextInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string description
        {
            get;
            set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// 处理地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string link
        {
            get;
            set;
        }
    }
}
