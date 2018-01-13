using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class ProxySwitcherViewModel : ViewModelBase
    {
        public MainViewModel MainPage;
        private ProxyModel _proxyDataModel;
        private ProxyModel _cachedProxyDataModel;
        private Proxy _activeProxy;

        public ProxySwitcherViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            _proxyDataModel = new ProxyModel();
            _cachedProxyDataModel = new ProxyModel();
        }

        public ProxyModel ProxyDataModel
        {
            get => _proxyDataModel;
            set => Set(() => ProxyDataModel, ref _proxyDataModel, value);
        }

        public Proxy ActiveProxy
        {
            get => _activeProxy;
            set => Set(() => ActiveProxy, ref _activeProxy, value);
        }

        public ProxyModel CachedProxyDataModel
        {
            get => _cachedProxyDataModel;
            set => Set(() => CachedProxyDataModel, ref _cachedProxyDataModel, value);
        }

        public async Task LoadProxy()
        {
            var response = await MainPage.HttpUtil.MakeRequestAsync(AccountModel.Url);
            ProxyModel proxyModel = JsonConvert.DeserializeObject<ProxyModel>(response);

            var data = (from newP in proxyModel.Proxies
                join oldP in ProxyDataModel.Proxies on newP.Proxies equals oldP.Proxies
                    into gj
                from subOld in gj.DefaultIfEmpty()
                select new Proxy
                {
                    Status = subOld?.Status ?? newP.Status,
                    Proxies = newP.Proxies,
                    Ip = newP.Ip,
                    Country = newP.Country,
                    City = newP.City,
                    Speed = newP.Speed,
                    Uptime = newP.Uptime
                }).ToList();

            ProxyDataModel = new ProxyModel {Proxies = new ObservableCollection<Proxy>(data)};
            CachedProxyDataModel.Proxies = new ObservableCollection<Proxy>(ProxyDataModel.Proxies);
            await MainPage.DialogPage.ExecuteFilter();
        }
    }
}