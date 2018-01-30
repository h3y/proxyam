using System;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Resources;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using System.Windows;
using proxyam.Helper;
using System.Threading.Tasks;

namespace proxyam.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel: ObservableObject
    {

        private ViewModelBase _currentPage;
        public LoginViewModel LoginPage { get; }
        public ErrorModalViewModel ErrorModalPage { get; }
        public ProxySwitcherViewModel ProxySwitcherPage { get; }
        public FilterViewModel FilterPage { get; }
        public DialogViewModel DialogPage { get; }
        public SplashScreenViewModel SplashScreenPage { get; }
        public SettingViewModel SettingPage { get; }
        public HttpUtil HttpUtil { get; }
        public ProxyChecker ProxyChecker { get; }
        public ConnectedProxyViewModel ConnectedProxyPage { get; }

        public MainViewModel()
        {
            ProxySwitcherPage = new ProxySwitcherViewModel(this);
            FilterPage = new FilterViewModel(this);
            SplashScreenPage = new SplashScreenViewModel();
            ErrorModalPage = new ErrorModalViewModel(this);
            DialogPage = new DialogViewModel(this);
            SettingPage = new SettingViewModel(this);
            HttpUtil = new HttpUtil(this);
            LoginPage = new LoginViewModel(this);
            ProxyChecker = new ProxyChecker(this);
            ConnectedProxyPage = new ConnectedProxyViewModel(this);
            CurrentPage = LoginPage;
        }

        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set => Set(() => CurrentPage, ref _currentPage, value);
        }

        public async Task SetHomePage()
        {
            await ProxySwitcherPage.LoadProxy();
            await ProxySwitcherPage.MainPage.FilterPage.InitFilter();
            CurrentPage = ProxySwitcherPage;
        }

        public string GetLanguageMessage(string key)
        {
            return (Application.Current.Resources[key] ?? "NULL").ToString();
        }

        
    }
}