using System;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;

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
        public SplashScreenViewModel SplashScreenPage { get; }
        public ICommand ProxySwitcherCommand { get; private set; }

        public MainViewModel()
        {
            LoginPage = new LoginViewModel(this);
            ProxySwitcherPage = new ProxySwitcherViewModel(this);
            SplashScreenPage = new SplashScreenViewModel();
            ErrorModalPage = new ErrorModalViewModel(this);
            CurrentPage = LoginPage;
            ProxySwitcherCommand = new RelayCommand(ExecuteProxySwitcherLayout);
        }

        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set => Set(() => CurrentPage, ref _currentPage, value);
        } 

        private void ExecuteProxySwitcherLayout()
        {
            CurrentPage = ProxySwitcherPage;
        }
    }
}