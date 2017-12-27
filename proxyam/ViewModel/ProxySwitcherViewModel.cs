using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
 
        public ProxySwitcherViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            _proxyDataModel = new ProxyModel();
        }

        public ProxyModel ProxyDataModel
        {
            get => _proxyDataModel;
            set => Set(() => ProxyDataModel, ref _proxyDataModel, value);
        }

        public async Task LoadProxy()
        {
          var response  = await MainPage.HttpUtil.MakeRequestAsync(AccountModel.Url);
          ProxyDataModel = JsonConvert.DeserializeObject<ProxyModel>(response);
        }

    }
}
