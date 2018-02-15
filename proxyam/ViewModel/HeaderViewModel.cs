using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using proxyam.Model;
using RestSharp;

namespace proxyam.ViewModel
{
    public class HeaderViewModel
    {
        public MainViewModel MainViewModel { get; }
        public AccountModel AccountModel { get; } = Singleton.AccountModel;
        private readonly string _api = Properties.Settings.Default.apiKey;

        public HeaderViewModel(MainViewModel main)
        {
            MainViewModel = main;
        }

        public void UpdateUserInformatio()
        {
            UpdateThreads();
            UpdateHeaderInformation();
        }

        private void UpdateThreads()
        {
            Thread worker = new Thread(() =>
            {
                var client = new RestClient($"http://api.proxy.am?key={_api}&threads");
                client.FollowRedirects = false;
                var request = new RestRequest(Method.GET);
                while (true)
                {
                    client.ExecuteAsync(request, resp => { AccountModel.ActiveThreads = resp.Content; });
                    Thread.Sleep(5000);
                }
            });
            worker.Name = "ActiveThreadUpdater";
            worker.IsBackground = true;
            worker.Start();
        }

        private void UpdateHeaderInformation()
        {
            Thread worker = new Thread(() =>
            {
                var client = new RestClient($"http://api.proxy.am?switch={_api}");
                client.FollowRedirects = false;
                var request = new RestRequest(Method.GET);
                while (true)
                {
                    client.ExecuteAsync(request,
                        resp => { AccountModel.FillData(JsonConvert.DeserializeObject<AccountModel>(resp.Content)); });
                    Thread.Sleep(15000);
                }
            });
            worker.Name = "UpdateHeaderInformation";
            worker.IsBackground = true;
            worker.Start();
        }
    }
}