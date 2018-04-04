using System;

using System.Windows.Input;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using proxyam.Helper;
using proxyam.Model;

namespace proxyam.ViewModel
{
    public class ConnectedProxyViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; }
        private Proxy _activeProxy;
        private Proxy.ProxyStatus _oldProxyStatus;
        public ICommand ConnectToProxyCommand { get; private set; }
        private readonly DispatcherTimer _timer;
        private TimeSpan _connecterProxyOnline;

        public ConnectedProxyViewModel(MainViewModel main)
        {
            MainViewModel = main;
            ConnectToProxyCommand = new RelayCommand<Proxy>(ConnectToProxyMethod);
            _timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            _timer.Tick += _timer_Tick;
        }

        public TimeSpan ConnecterProxyOnline
        {
            get => _connecterProxyOnline;
            set => Set(() => ConnecterProxyOnline, ref _connecterProxyOnline, value);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            ConnecterProxyOnline = TimeSpan.FromSeconds(ConnecterProxyOnline.Seconds + 1);
        }

        private void ConnectToProxyMethod(Proxy proxy)
        {
            if (ActiveProxy != null)
            {
                if (ActiveProxy == proxy)
                {
                    ActiveProxy.Status = _oldProxyStatus;
                    ActiveProxy = null;
                    StopTimer();
					RunProxyCh.Stop();

					return;
                }

                ActiveProxy.Status = _oldProxyStatus;
            }


            ActiveProxy = proxy;
            _oldProxyStatus = proxy.Status;
            ActiveProxy.Status = Proxy.ProxyStatus.Connected;
            StartTimer();
			RunProxyCh.Start(ActiveProxy.Proxies);
		}

        public Proxy ActiveProxy
        {
            get => _activeProxy;
            set => Set(() => ActiveProxy, ref _activeProxy, value);
        }

        private void StartTimer()
        {
            _timer.Stop();
            ConnecterProxyOnline = TimeSpan.FromSeconds(0);
            _timer.Start();
        }

        private void StopTimer()
        {
            _timer.Stop();
        }
    }
}