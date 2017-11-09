using System;
using System.IO;
using System.Net;
using System.Threading;
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
        private Account _account;
        public MainViewModel MainPage { get; }

        public LoginViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            LogInCommand = new RelayCommand<string>(LogInMehod);
        }

        public Account AccountModel
        {
            get => _account;
            set => Set(() => AccountModel, ref _account, value);
        }


        private async void LogInMehod(string apiKey)
        {
            MainPage.SplashScreenPage.SetShowSplash(true);
            var response = await MakeRequestAsync($"https://proxy.am/api.php?switch={apiKey}");

            if (response == null)
                ShowErrorWindow("InternetFailure");
            else
            {
                switch (response)
                {
                    case "":
                        ShowErrorWindow("LoginEmptyMessage");
                        break;
                    case "err0":
                        ShowErrorWindow("LoginWrongApi");
                        break;
                    case "err1":
                        ShowErrorWindow("LoginEndTariff");
                        System.Diagnostics.Process.Start("https://proxy.am/");
                        break;
                    case "err2":
                        ShowErrorWindow("LoginBuyTariff");
                        System.Diagnostics.Process.Start("https://proxy.am/");
                        break;
                    default:
                        AccountModel = JsonConvert.DeserializeObject<Account>(response);
                        switch (AccountModel.Status.ToLower())
                        {
                            case "ok":
                                MainPage.SetHomePage();
                                break;
                            case "err":
                                ShowErrorWindow("LoginErrorTariff");
                                break;
                            case "wait":
                                ShowErrorWindow("LoginWaitTariff");
                                break;
                            default:
                                ShowErrorWindow("LoginErrorTariff", $": {AccountModel.Status.ToLower()}");
                                break;
                        }
                        break;
                }
            }
            MainPage.SplashScreenPage.SetShowSplash(false);
        }

        private async Task<String> MakeRequestAsync(string url)
        {
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            String responseText = await Task.Run(() =>
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    WebResponse response = request?.GetResponse();
                    Stream responseStream = response?.GetResponseStream();
                    return new StreamReader(responseStream ?? throw new WebException()).ReadToEnd();
                }
                catch (WebException ex)
                {
                    switch (ex.Status)
                    {
                        case WebExceptionStatus.UnknownError:
                            ShowErrorWindow("UnknownError");
                            break;

                        case WebExceptionStatus.ProtocolError:
                            ShowErrorWindow("ProtocolError", ((ex.Response as HttpWebResponse)?.StatusCode).ToString());
                            break;

                        case WebExceptionStatus.ConnectFailure:
                            ShowErrorWindow("ConnectFailure");
                            break;

                        case WebExceptionStatus.SendFailure:
                            ShowErrorWindow("SendFailure");
                            break;

                        case WebExceptionStatus.ReceiveFailure:
                            ShowErrorWindow("ReceiveFailure");
                            break;
                    }
                }
                return null;
            });
            return responseText;
        }

        private void ShowErrorWindow(string message, string additionalMessage = "")
        {
            MainPage.ErrorModalPage.ShowErrorModalCommand.Execute(
                MainPage.GetLanguageMessage(message) + additionalMessage);
        }
    }
}