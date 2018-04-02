using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class FilterViewModel : ViewModelBase
    {
        public MainViewModel MainPage { get; }
        private FilterModel _filterModel;
        public ICommand SelectAllProxyCommand { get; private set; }
        public ICommand UnSelectAllProxyCommand { get; private set; }
        public ICommand ClearSpeedCommand { get; private set; }
        public ICommand ClearUptimeCommand { get; private set; }
        public ICommand ClearCitysCommand { get; private set; }

        public FilterViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            _filterModel = new FilterModel();
            SelectAllProxyCommand = new RelayCommand(SelectAllProxyMethod);
            UnSelectAllProxyCommand = new RelayCommand(UnSelectAllProxyMethod);
            ClearSpeedCommand = new RelayCommand(ClearSpeedMethod);
            ClearUptimeCommand = new RelayCommand(ClearUptimeMethod);
            ClearCitysCommand = new RelayCommand(ClearCitysMethod);
        }

        private async void ClearCitysMethod()
        {
            await Task.Run(() => { FilterModel.City = string.Empty; });
        }

        private async void ClearUptimeMethod()
        {
            await Task.Run(() =>
            {
                FilterModel.MaxUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Uptime);
                FilterModel.MinUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Uptime);
                FilterModel.CurrentMaxUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Uptime);
                FilterModel.CurrentMinUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Uptime);
            });
        }

        private async void ClearSpeedMethod()
        {
            await Task.Run(() =>
            {
                FilterModel.MaxSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Speed);
                FilterModel.MinSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Speed);
                FilterModel.CurrentMaxSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Speed);
                FilterModel.CurrentMinSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Speed);
            });
        }

        public FilterModel FilterModel
        {
            get => _filterModel;
            set => Set(() => FilterModel, ref _filterModel, value);
        }

        public async Task InitFilter()
        {
            await Task.Run(() =>
            {
                FilterModel.Country = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies
                    .GroupBy(a => a.Country).ToDictionary(
                        gdc => gdc.Key,
                        gdc => new FilterModel.CountryCount
                        {
                            Count = gdc.Count(),
                            IsChecked = true
                        });

                FilterModel.MaxSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Speed);
                FilterModel.MinSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Speed);
                FilterModel.MaxUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Uptime);
                FilterModel.MinUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Uptime);
                FilterModel.CurrentMaxSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Speed);
                FilterModel.CurrentMinSpeed = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Speed);
                FilterModel.CurrentMaxUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Max(a => a.Uptime);
                FilterModel.CurrentMinUptime = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Min(a => a.Uptime);
				MainPage.SettingPage.LoadAppSettingsCommand.Execute(null);
            });
        }

        private async void SelectAllProxyMethod()
        {
            await Task.Run(() =>
            {
                foreach (var count in FilterModel.Country)
                {
                    count.Value.IsChecked = true;
                }
            });
        }

        private async void UnSelectAllProxyMethod()
        {
            await Task.Run(() =>
            {
                foreach (var count in FilterModel.Country)
                {
                    count.Value.IsChecked = false;
                }
            });
        }
    }
}