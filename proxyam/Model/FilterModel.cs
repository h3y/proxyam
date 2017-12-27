using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proxyam.Model
{
    public class FilterModel: ObservableObject, IDisposable
    {
        private Dictionary<string,int> _country = new Dictionary<string, int>();
        private double _minSpeed;
        private double _maxSpeed;
        private double _minUptime;
        private double _maxUptime;
        private List<string> _city = new List<string>();

        public Dictionary<string, int> Country
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

        public List<string> City
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
            City.Clear();
        }
    }
}
