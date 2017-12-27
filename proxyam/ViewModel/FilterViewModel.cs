using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class FilterViewModel : ViewModelBase
    {
        public MainViewModel MainPage { get; }
        private FilterModel _filterModel;

        public FilterViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            _filterModel = new FilterModel();
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
                FilterModel.Country = MainPage.MainPage.ProxyDataModel.Proxies.GroupBy(a => a.Country).ToDictionary(gdc => gdc.Key, gdc => gdc.Count());
                FilterModel.MaxSpeed = MainPage.MainPage.ProxyDataModel.Proxies.Max(a => a.Speed);
                FilterModel.MinSpeed = MainPage.MainPage.ProxyDataModel.Proxies.Min(a => a.Speed);
                FilterModel.MaxUptime = MainPage.MainPage.ProxyDataModel.Proxies.Max(a => a.Uptime);
                FilterModel.MinUptime = MainPage.MainPage.ProxyDataModel.Proxies.Min(a => a.Uptime);
            });
            RaisePropertyChanged(()=> FilterModel.Country);
        }
    }
}