using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components.RSS
{
    public class RSSSource
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string title
        {
            get;
            set;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url
        {
            get;
            set;
        }
    }
}
