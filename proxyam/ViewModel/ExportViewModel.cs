using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class ExportViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; }

        private IDictionary<string, bool> _proxyType;

        public ExportViewModel(MainViewModel main)
        {
            MainViewModel = main;
            _proxyType = new Dictionary<string, bool>();
            FillProxyType();
        }

        public IDictionary<string, bool> ProxyType
        {
            get => _proxyType;
            set => Set(() => ProxyType, ref _proxyType, value);
        }

        private void FillProxyType()
        {
            ProxyType.Add("proxies", false);
            ProxyType.Add("ip", false);
            ProxyType.Add("country", false);
            ProxyType.Add("city", false);
            ProxyType.Add("speed", false);
            ProxyType.Add("uptime", false);
        }
    }
}