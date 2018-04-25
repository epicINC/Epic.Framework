using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    /// <summary>
    /// RSS 文档
    /// 参看 http://www.rssboard.org/rss-specification
    /// </summary>

    [JsonObject(Id = "rss", ItemRequired = Required.Always)]
    public class RSSDcoument : JsonObject
    {
        public RSSDcoument()
        {
            this.channel = new List<RSSChannel>();
        }


        [JsonProperty(Required = Required.Always)]
        public List<RSSChannel> channel
        {
            get;
            set;
        }
    }
}
