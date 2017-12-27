using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace proxyam.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ProxyModel
    {
        [JsonProperty("data")]
        public ObservableCollection<Proxy> Proxies { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class Proxy : ObservableObject
    {
        public Proxy(ProxyStatus status, string proxies, string ip, string country, string city, double speed, double uptime)
        {
            _status = status;
            Proxies = proxies;
            Ip = ip;
            Country = country;
            City = city;
            Speed = speed;
            Uptime = uptime;
        }

        public enum ProxyStatus
        {
            Unknown,
            Connected,
            Bad,
            Successful
        };

        private ProxyStatus _status;
        [JsonProperty("proxy")]
        public string Proxies { get; set; }
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("uptime")]
        public double Uptime { get; set; }

        public ProxyStatus Status
        {
            get => _status;
            set => Set(() => Status, ref _status, value);
        }
    }
}