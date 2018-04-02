using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using proxyam.Model;
using proxyam.View;

namespace proxyam.ViewModel
{
    public class DialogViewModel : ObservableObject
    {
        public MainViewModel MainPage { get; }
        public ICommand RunExtendedDialogCommand => new RelayCommand<Button>(ExecuteRunExtendedDialog);
        private ViewModelBase _template = null;

        public DialogViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
        }

        private async void ExecuteRunExtendedDialog(Button button)
        {
            switch (button.Name)
            {
                case "Setting":
                    _template = MainPage.SettingPage;
                    break;
                case "Filter":
                    _template = MainPage.FilterPage;
                    break;
                case "Export":
                    _template = MainPage.ExportPage;
                    break;
            }

            //show the dialog
            var result = await DialogHost.Show(_template, "RootDialog", ExtendedOpenedEventHandler,
                ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
            
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter == null || Boolean.Parse(eventArgs.Parameter.ToString()) == false)
                return;


            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SplashScreen());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            switch (_template)
            {
                case FilterViewModel _:
                    await ExecuteFilter();
                    break;
                case SettingViewModel _:
                    break;
            }

			
			eventArgs.Session.Close(false);
        }

        //TODO in future to FilterViewModel
        public async Task ExecuteFilter()
        {
            if (MainPage.FilterPage.FilterModel.Country.Count == 0)
                return;
            await Task.Run(() =>
            {
                //filter by country
                var tmpProxies = new List<Proxy>();
                var filterModel = MainPage.FilterPage.FilterModel;

                foreach (var country in filterModel.Country.AsParallel())
                {
                    if (!country.Value.IsChecked) continue;
                    tmpProxies.AddRange(MainPage.ProxySwitcherPage.ProxyDataModel.Proxies
                        .Where(a => a.Country == country.Key).AsParallel());
                }

                tmpProxies = tmpProxies.AsParallel().Where(a =>
                    a.Speed >= filterModel.MinSpeed
                    && a.Speed <= filterModel.MaxSpeed
                    && a.Uptime >= filterModel.MinUptime
                    && a.Uptime <= filterModel.MaxUptime
                ).ToList();

                tmpProxies = tmpProxies.AsParallel().Where(a =>
                    a.Speed >= filterModel.MinSpeed
                    && a.Speed <= filterModel.MaxSpeed
                    && a.Uptime >= filterModel.MinUptime
                    && a.Uptime <= filterModel.MaxUptime
                ).ToList();

                var proxyWithCity = new List<Proxy>();

                if (string.IsNullOrWhiteSpace(filterModel.City))
                {
                    proxyWithCity.AddRange(tmpProxies);
                }
                else
                {
                    foreach (var city in filterModel.City.Split(';').ToArray())
                    {
                        proxyWithCity.AddRange(
                            tmpProxies
                                .AsParallel()
                                .Where(a => a.City.ToLower().Trim() == city.ToLower().Trim())
                                .ToList()
                                .AsParallel()
                        );
                    }
                }

                MainPage.ProxySwitcherPage.CachedProxyDataModel =
                    new ProxyModel
                    {
                        Proxies = new ObservableCollection<Proxy>(proxyWithCity)
                    };
            });
        }
    }
}