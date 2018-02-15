using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ClosedXML.Excel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class KeyValue : ViewModelBase
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
        public ICommand SaveTxtFileCommand => new RelayCommand(SaveTxtFileMethod);
        public ICommand SaveXlsxFileCommand => new RelayCommand(SaveXlsxFileMethod);

        private async void SaveXlsxFileMethod()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Save proxy to excel file",
                CheckPathExists = true,
                AddExtension = true,
                FileName = "proxy.xlsx",
                DefaultExt = "xlsx",
                Filter = "Text files (*.xlsx)|*.xlsx",
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() != true) return;

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample Sheet");
            worksheet.Cell("A1").Value = "Hello World!";
            worksheet.Cell("A1").Value = "Hello World!";
            workbook.SaveAs("HelloWorld.xlsx");
        }

        private async void SaveTxtFileMethod()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Save proxy to file",
                CheckPathExists = true,
                AddExtension = true,
                FileName = "proxy.txt",
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt",
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() != true) return;

            using (var stream = new StreamWriter(saveFileDialog.FileName,false))
            {
                var line = new List<string>();
                foreach (var proxy in MainViewModel.ProxySwitcherPage.CachedProxyDataModel.Proxies)
                {
                    if (ProxyType[0].Value)
                        line.Add(proxy.Proxies);
                    if (ProxyType[1].Value)
                        line.Add(proxy.Ip);
                    if (ProxyType[2].Value)
                        line.Add(proxy.Country);
                    if (ProxyType[3].Value)
                        line.Add(proxy.City);
                    if (ProxyType[4].Value)
                        line.Add(proxy.Speed.ToString(CultureInfo.InvariantCulture));
                    if (ProxyType[5].Value)
                        line.Add(proxy.Uptime.ToString(CultureInfo.InvariantCulture));

                    await stream.WriteLineAsync(line.Aggregate((a, b) => a + ',' + b));
                    line.Clear();
                }
            }
        }

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