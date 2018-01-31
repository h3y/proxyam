using Newtonsoft.Json;
using System;
using GalaSoft.MvvmLight;

namespace proxyam.Model
{
    public static class Singleton
    {
        private static readonly Lazy<AccountModel> Lazy = new Lazy<AccountModel>(() => new AccountModel());

        public static AccountModel AccountModel
        {
            get => Lazy.Value;
        }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class AccountModel : ObservableObject
    {
        private string _tarif;
        private string _proxyCount;
        private string _threads;
        private string _status;
        private string _end;
        private string _url;
        private string _activeThreads = "0";

        public void FillData(AccountModel accountModel)
        {
            Tarif = accountModel.Tarif;
            ProxyCount = accountModel.ProxyCount;
            Threads = accountModel.Threads;
            Status = accountModel.Status;
            End = accountModel.End;
            Url = accountModel.Url;
        }

        [JsonProperty("tarif")]
        public string Tarif
        {
            get => _tarif;
            set => Set(() => Tarif, ref _tarif, value);
        }

        [JsonProperty("all")]
        public string ProxyCount
        {
            get => _proxyCount;
            set => Set(() => ProxyCount, ref _proxyCount, value);
        }

        [JsonProperty("flows")]
        public string Threads
        {
            get => _threads;
            set => Set(() => Threads, ref _threads, value);
        }

        [JsonProperty("upd")]
        public string Status
        {
            get => _status;
            set => Set(() => Status, ref _status, value);
        }

        [JsonProperty("end")]
        public string End
        {
            get => _end;
            set => Set(() => End, ref _end, value);
        }

        [JsonProperty("url")]
        public string Url
        {
            get => _url;
            set => Set(() => Url, ref _url, value);
        }

        public string ActiveThreads
        {
            get => _activeThreads;
            set => Set(() => ActiveThreads, ref _activeThreads, value);
        }
    }
}