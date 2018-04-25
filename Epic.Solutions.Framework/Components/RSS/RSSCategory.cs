using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    public class RSSCategory
    {
        // 必填元素 Required

        public string title
        {
            get;
            set;
        }

        // 可选元素 Optional

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string domain
        {
            get;
            set;
        }
    }
}
