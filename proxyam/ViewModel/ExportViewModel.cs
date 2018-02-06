using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class KeyValue: ViewModelBase
    {
        private string _key;
        private bool _value;

        public KeyValue()
        {
          
        }

        public KeyValue(string key, bool value)
        {
            Key = key;
            Value = value;
        }

        public string Key
        {
            get { return _key; }
            set => Set(() => Key, ref _key, value);
        }

        public bool Value
        {
            get { return _value; }
            set => Set(() => Value, ref _value, value);
        }
    }

    public class ExportViewModel : KeyValue
    {
        public MainViewModel MainViewModel { get; }

        private ObservableCollection<KeyValue> _proxyType;

        public ExportViewModel(MainViewModel main)
        {
            MainViewModel = main;
            _proxyType = new ObservableCollection<KeyValue>();
            FillProxyType();
        }

        public ObservableCollection<KeyValue> ProxyType
        {
            get => _proxyType;
            set => Set(() => ProxyType, ref _proxyType, value);
        }

        private void FillProxyType()
        {
            ProxyType.Add(new KeyValue("proxies", true));
            ProxyType.Add(new KeyValue("ip", true));
            ProxyType.Add(new KeyValue("country", true));
            ProxyType.Add(new KeyValue("city", true));
            ProxyType.Add(new KeyValue("speed", true));
            ProxyType.Add(new KeyValue("uptime", true));
        }
    }
}