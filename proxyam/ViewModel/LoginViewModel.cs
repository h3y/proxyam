using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand LogInCommand { get; private set; }
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

            switch (response)
            {
                case "": break;
                case "err0":
                    MainPage.ErrorModalPage.ShowErrorModalCommand.Execute("Buy Tariff");

                    break;
                case "err1":
                    MainPage.ErrorModalPage.ShowErrorModalCommand.Execute("Buy Tariff");
                    System.Diagnostics.Process.Start("https://proxy.am/");
                    break;
                case "err2":
                    MainPage.ErrorModalPage.ShowErrorModalCommand.Execute("Buy Tariff");
                    System.Diagnostics.Process.Start("https://proxy.am/");
                    break;
                default:
                    AccountModel = JsonConvert.DeserializeObject<Account>(response);
                    switch (AccountModel.Status.ToLower())
                    {
                        case "ok":
                            MainPage.ProxySwitcherCommand.Execute(null);
                            break;
                        case "err":
                            MainPage.ErrorModalPage.ShowErrorModalCommand.Execute("Buy Tariff");
                            break;
                        case "wait":
                            MainPage.ErrorModalPage.ShowErrorModalCommand.Execute("Buy Tariff");
                            break;
                        default:
                            MainPage.ErrorModalPage.ShowErrorModalCommand.Execute(
                                "ErrorMess" + AccountModel.Status.ToLower());
                            break;
                    }
                    break;
            }
            MainPage.SplashScreenPage.SetShowSplash(false);
        }

        private async Task<String> MakeRequestAsync(string url)
        {
            String responseText = await Task.Run(() =>
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    WebResponse response = request?.GetResponse();
                    Stream responseStream = response?.GetResponseStream();
                    Thread.Sleep(1000);
                    return new StreamReader(responseStream).ReadToEnd();
                }
                catch (WebException ex)
                {
                    switch (ex.Status)
                    {
                        case WebExceptionStatus.UnknownError:
                            MessageBox.Show("Неизвестная ошибка", "Ошибка");
                            break;

                        case WebExceptionStatus.ProtocolError:
                            MessageBox.Show($"Код состояния: {(ex.Response as HttpWebResponse)?.StatusCode}", "Ошибка");
                            break;

                        case WebExceptionStatus.ConnectFailure:
                            MessageBox.Show("Не удалось соединиться с HTTP-сервером.", "Ошибка");
                            break;

                        case WebExceptionStatus.SendFailure:
                            MessageBox.Show("Не удалось отправить запрос HTTP-серверу.", "Ошибка");
                            break;

                        case WebExceptionStatus.ReceiveFailure:
                            MessageBox.Show("Не удалось загрузить ответ от HTTP-сервера.", "Ошибка");
                            break;
                    }
                }
                return null;
            });
            return responseText;
        }
    }
}