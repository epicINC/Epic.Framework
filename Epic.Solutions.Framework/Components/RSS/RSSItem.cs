using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    public class RSSItem
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
        /// 链接
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string link
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
        /// 作者
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string author
        {
            get;
            set;
        }

        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSCategory category
        {
            get;
            set;
        }

        /// <summary>
        /// 当前页面关联的 评论地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string comments
        {
            get;
            set;
        }

        /// <summary>
        /// 附件
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSEnclosure enclosure
        {
            get;
            set;
        }

        /// <summary>
        /// 文章 识别号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string guid
        {
            get;
            set;
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime pubDate
        {
            get;
            set;
        }

        /// <summary>
        /// 内容来源频道
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSSource source
        {
            get;
            set;
        }
    }
}
