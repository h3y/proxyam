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


        public MainViewModel()
        {
            LoginPage = new LoginViewModel(this);
            MainPage = new ProxySwitcherViewModel(this);
            SplashScreenPage = new SplashScreenViewModel();
            ErrorModalPage = new ErrorModalViewModel(this);
            DialogPage = new DialogViewModel(this);
            SettingPage = new SettingViewModel(this);
            CurrentPage = LoginPage;
        }

        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set => Set(() => CurrentPage, ref _currentPage, value);
        } 

        public void SetHomePage()
        {
            CurrentPage = MainPage;
        }

        public string GetLanguageMessage(string key)
        {
            return (Application.Current.Resources[key] ?? "NULL").ToString();
        }

        
    }
}