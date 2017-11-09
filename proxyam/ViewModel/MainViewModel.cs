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
        public ProxySwitcherViewModel MainPage { get; }
        public DialogViewModel DialogPage { get; }
        public SplashScreenViewModel SplashScreenPage { get; }
        public SettingViewModel SettingPage { get; }
        public HttpUtil HttpUtil { get; }


        public MainViewModel()
        {
            LoginPage = new LoginViewModel(this);
            MainPage = new ProxySwitcherViewModel(this);
            SplashScreenPage = new SplashScreenViewModel();
            ErrorModalPage = new ErrorModalViewModel(this);
            DialogPage = new DialogViewModel(this);
            SettingPage = new SettingViewModel(this);
            HttpUtil = new HttpUtil(this);
            CurrentPage = LoginPage;
        }

        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set => Set(() => CurrentPage, ref _currentPage, value);
        } 

        public void SetHomePage()
        {
            MainPage.LoadProxy();
            CurrentPage = MainPage;
        }

        public string GetLanguageMessage(string key)
        {
            return (Application.Current.Resources[key] ?? "NULL").ToString();
        }

        
    }
}