using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxyam.Model
{
    public class FilterModel : ObservableObject, IDisposable
    {
        public class CountryCount : ObservableObject
        {
            private bool _isChecked;

            public bool IsChecked
            {
                get => _isChecked;
                set => Set(() => IsChecked, ref _isChecked, value);
            }

            public int Count { get; set; }
        }

        private Dictionary<string, CountryCount> _country = new Dictionary<string, CountryCount>();

        private double _minSpeed;
        private double _maxSpeed;
        private double _minUptime;
        private double _maxUptime;

        private string _city = "";

        public Dictionary<string, CountryCount> Country
        {
            get => _country;
            set => Set(() => Country, ref _country, value);
        }

        public double MinSpeed
        {
            get => _minSpeed;
            set => Set(() => MinSpeed, ref _minSpeed, value);
        }

        public double MaxSpeed
        {
            get => _maxSpeed;
            set => Set(() => MaxSpeed, ref _maxSpeed, value);
        }

        public double MinUptime
        {
            get => _minUptime;
            set => Set(() => MinUptime, ref _minUptime, value);
        }

        public double MaxUptime
        {
            get => _maxUptime;
            set => Set(() => MaxUptime, ref _maxUptime, value);
        }

        public string City
        {
            get => _city;
            set => Set(() => City, ref _city, value);
        }

        public void Dispose()
        {
            Country.Clear();
            MinSpeed = 0;
            MaxSpeed = 0;
            MinUptime = 0;
            MaxUptime = 0;
            City = "";
        }


        private double _currentMinSpeed;
        private double _currentMaxSpeed;
        private double _currentMinUptime;
        private double _currentMaxUptime;

        public double CurrentMinSpeed
        {
            get => _currentMinSpeed;
            set => Set(() => CurrentMinSpeed, ref _currentMinSpeed, value);
        }

        public double CurrentMaxSpeed
        {
            get => _currentMaxSpeed;
            set => Set(() => CurrentMaxSpeed, ref _currentMaxSpeed, value);
        }

        public double CurrentMinUptime
        {
            get => _currentMinUptime;
            set => Set(() => CurrentMinUptime, ref _currentMinUptime, value);
        }

        public double CurrentMaxUptime
        {
            get => _currentMaxUptime;
            set => Set(() => CurrentMaxUptime, ref _currentMaxUptime, value);
        }
    }
}