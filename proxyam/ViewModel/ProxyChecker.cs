using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using proxyam.Model;
using RestSharp;

namespace proxyam.ViewModel
{
    public class ProxyChecker : ObservableObject
    {
        public readonly MainViewModel MainPage;
        public Task CheckingTask;
        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        public List<Thread> ThreadPool { get; private set; }
        public ICommand CheckProxyCommand { get; private set; }
        public ICommand CheckSelectedProxyCommand { get; private set; }
        private readonly object _threadLocker = new object();

        private int _goodProxyCount;
        private int _badProxyCount;
        private int _currentProxyIndex;

        public int GoodProxyCount
        {
            get => _goodProxyCount;
            set => Set(() => GoodProxyCount, ref _goodProxyCount, value);
        }

        public int BadProxyCount
        {
            get => _badProxyCount;
            set => Set(() => BadProxyCount, ref _badProxyCount, value);
        }

        public int CurrentProxyIndex
        {
            get => _currentProxyIndex;
            set => Set(() => CurrentProxyIndex, ref _currentProxyIndex, value);
        }

        public ProxyChecker(MainViewModel mainPage)
        {
            MainPage = mainPage;
            CheckProxyCommand = new RelayCommand<object>(ExecuteProxyCommand);
            CheckSelectedProxyCommand = new RelayCommand<Proxy>(CheckSelectedProxyMethod);
            ThreadPool = new List<Thread>();
        }

        private void CheckProxy(Proxy data)
        {
            if (data.Status == Proxy.ProxyStatus.Successful || data.Status == Proxy.ProxyStatus.Connected)
            {
                lock (_threadLocker)
                {
                    GoodProxyCount++;
                }

                return;
            }

            var client = new RestClient("http://api.proxy.am");
            var proxy = data.Proxies.Split(':');
            client.Proxy = new WebProxy(proxy[0], int.Parse(proxy[1]));
            client.FollowRedirects = false;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Connection", "close");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                lock (_threadLocker)
                {
                    GoodProxyCount++;
                    data.Status = Proxy.ProxyStatus.Successful;
                }
            }
            else
            {
                lock (_threadLocker)
                {
                    BadProxyCount++;
                    data.Status = Proxy.ProxyStatus.Bad;
                }
            }
        }

        private void CheckSelectedProxyMethod(Proxy proxy)
        {
           Task.Run(()=> CheckProxy(proxy));
        }

        private void AsyncCheckingProxys()
        {
            var obj = _threadLocker;
            int countProxies = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Count;
            while (!_cancelTokenSource.Token.IsCancellationRequested)
            {
                Proxy proxy;
                lock (obj)
                {
                    if (CurrentProxyIndex >= countProxies)
                    {
                        _cancelTokenSource.Cancel();
                        return;
                    }

                    proxy = MainPage.ProxySwitcherPage.ProxyDataModel.Proxies[CurrentProxyIndex++];
                }

                CheckProxy(proxy);
            }
        }

        private async void ExecuteProxyCommand(object button)
        {
            var checkProxyButton = ((Button) button);

            if (!checkProxyButton.IsEnabled || MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Count == 0)
                return;

            checkProxyButton.Content = "Wait!";
            checkProxyButton.IsEnabled = false;

            ThreadPool.Clear();
            CurrentProxyIndex = 0;
            GoodProxyCount = 0;
            BadProxyCount = 0;
            _cancelTokenSource = new CancellationTokenSource();

            for (int i = 0; i < 100; i++)
            {
                if (i >= MainPage.ProxySwitcherPage.ProxyDataModel.Proxies.Count)
                    return;
                ThreadPool.Add(new Thread(AsyncCheckingProxys));
                ThreadPool[i].IsBackground = true;
                ThreadPool[i].Start();
            }

            await Task.Run(() =>
            {
                while (!_cancelTokenSource.Token.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    checkProxyButton.Content = "CheckProxy";
                    checkProxyButton.IsEnabled = true;
                });
            });
           

        }
    }
}