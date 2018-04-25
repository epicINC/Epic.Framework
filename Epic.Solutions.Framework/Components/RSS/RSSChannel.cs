using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{

    [JsonObject(Id = "channel", ItemRequired = Required.Always)]
    public class RSSChannel
    {
        public RSSChannel()
        {
            this.item = new List<RSSItem>();
        }


        // 必填元素 Required

        /// <summary>
        /// 频道标题 Required
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 频道地址 Required
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string link
        {
            get;
            set;
        }

        /// <summary>
        /// 频道描述 Required
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string description
        {
            get;
            set;
        }


        // 可选元素 Optional

        /// <summary>
        /// 语言
        /// </summary>
        /// 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string language
        {
            get;
            set;
        }

        /// <summary>
        /// 版权声明
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string copyright
        {
            get;
            set;
        }

        /// <summary>
        /// 内容管理员联系 Email
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string managingEditor
        {
            get;
            set;
        }

        /// <summary>
        /// 技术管理员联系 Email
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string webMaster
        {
            get;
            set;
        }

        /// <summary>
        /// 内容发布日期
        /// 参看 RFC822 http://asg.web.cmu.edu/rfc/rfc822.html
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime pubDate
        {
            get;
            set;
        }

        /// <summary>
        /// 最后变更时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string lastBuildDate
        {
            get;
            set;
        }

        /// <summary>
        /// 定义频道分类(可多个)
        /// 参看 <item>-level category element http://www.rssboard.org/rss-specification#ltcategorygtSubelementOfLtitemgt
        /// more info http://www.rssboard.org/rss-specification#syndic8
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSCategory category
        {
            get;
            set;
        }

        /// <summary>
        /// 生成该频道的程序
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string generator
        {
            get;
            set;
        }

        /// <summary>
        /// 本页地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string docs
        {
            get;
            set;
        }

        /// <summary>
        /// 定义一个云端地址, 用于频道的更新通知
        /// 参看 http://www.rssboard.org/rss-specification#ltcloudgtSubelementOfLtchannelgt
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSCloud cloud
        {
            get;
            set;
        }

        /// <summary>
        /// 存活时间(定义缓存时间 单位分钟)
        /// 参看 http://www.rssboard.org/rss-specification#ltttlgtSubelementOfLtchannelgt
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ttl
        {
            get;
            set;
        }

        /// <summary>
        /// 频道图片 GIF JPEG PNG
        /// 参看 http://www.rssboard.org/rss-specification#ltimagegtSubelementOfLtchannelgt
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSImage image
        {
            get;
            set;
        }

        /// <summary>
        /// 频道分级
        /// 参看 PISC http://www.w3.org/PICS/
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string rating
        {
            get;
            set;
        }

        /// <summary>
        /// 定义输入框
        /// 参看 http://www.rssboard.org/rss-specification#lttextinputgtSubelementOfLtchannelgt
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RSSTextInput textInput
        {
            get;
            set;
        }

        /// <summary>
        /// 订阅器忽略小时
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string skipHours
        {
            get;
            set;
        }

        /// <summary>
        /// 订阅器忽略天数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string skipDays
        {
            get;
            set;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<RSSItem> item
        {
            get;
            set;
        }
    }
}
