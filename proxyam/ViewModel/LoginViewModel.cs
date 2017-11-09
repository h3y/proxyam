using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LogInCommand { get; }

        public MainViewModel MainPage { get; }

        public LoginViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            LogInCommand = new RelayCommand<string>(LogInMehod);
        }

        private async void LogInMehod(string apiKey)
        {
            MainPage.SplashScreenPage.SetShowSplash(true);
            var response = await MainPage.HttpUtil.MakeRequestAsync($"https://proxy.am/api.php?switch={apiKey}",1.5);

            if (response == null)
                MainPage.HttpUtil.ShowErrorWindow("InternetFailure");
            else
            {
                switch (response)
                {
                    case "":
                        MainPage.HttpUtil.ShowErrorWindow("LoginEmptyMessage");
                        break;
                    case "err0":
                        MainPage.HttpUtil.ShowErrorWindow("LoginWrongApi");
                        break;
                    case "err1":
                        MainPage.HttpUtil.ShowErrorWindow("LoginEndTariff");
                        System.Diagnostics.Process.Start("https://proxy.am/");
                        break;
                    case "err2":
                        MainPage.HttpUtil.ShowErrorWindow("LoginBuyTariff");
                        System.Diagnostics.Process.Start("https://proxy.am/");
                        break;
                    default:
                        JsonConvert.DeserializeObject<AccountModel>(response);
                        switch (AccountModel.Status.ToLower())
                        {
                            case "ok":
                                MainPage.SetHomePage();
                                break;
                            case "err":
                                MainPage.HttpUtil.MainPage.HttpUtil.ShowErrorWindow("LoginErrorTariff");
                                break;
                            case "wait":
                                MainPage.HttpUtil.ShowErrorWindow("LoginWaitTariff");
                                break;
                            default:
                                MainPage.HttpUtil.ShowErrorWindow("LoginErrorTariff", $": {AccountModel.Status.ToLower()}");
                                break;
                        }
                        break;
                }
            }
            MainPage.SplashScreenPage.SetShowSplash(false);
        }
    }
}