using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxyam.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Account
    {
        [JsonProperty("tarif")]
        public string Tarif { get; set; }

        [JsonProperty("all")]
        public string ProxyCount { get; set; }

        [JsonProperty("flows")]
        public string Threads { get; set; }

        [JsonProperty("upd")]
        public string Status { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}