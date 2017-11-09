using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxyam.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class AccountModel
    {
        [JsonProperty("tarif")]
        public static string Tarif { get; set; }

        [JsonProperty("all")]
        public static string ProxyCount { get; set; }

        [JsonProperty("flows")]
        public static string Threads { get; set; }

        [JsonProperty("upd")]
        public static string Status { get; set; }

        [JsonProperty("end")]
        public static string End { get; set; }

        [JsonProperty("url")]
        public static string Url { get; set; }
    }
}