using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using proxyam.Model;

namespace proxyam.ViewModel
{
	public class SettingViewModel:ViewModelBase
	{
		public ICommand SelectLanguageCommand { get; }
		public ICommand SaveAppSettingsCommand { get; }
		public ICommand LoadAppSettingsCommand { get; }
		private readonly MainViewModel _mainPage;
		private readonly string _settingFileName = "settings.json";
		private bool _saveSettings = true;

		public SettingViewModel(MainViewModel mainPage)
		{
			_mainPage = mainPage;
			SelectLanguageCommand = new RelayCommand<ComboBoxItem>(Execute);
			SaveAppSettingsCommand = new RelayCommand(SaveAppSettingsMethod);
			LoadAppSettingsCommand = new RelayCommand(LoadAppSettingsMethod);
		}

		private void Execute(ComboBoxItem selectedItem)
		{
			switch (selectedItem.Content.ToString())
			{
				case "Русский":
					App.Language = App.Languages[1];
					break;
				case "English":
					App.Language = App.Languages[0];
					break;
				default: break;
			}
		}

		public bool SaveSettings
		{
			get => _saveSettings;
			set => Set(() => SaveSettings, ref _saveSettings, value);
		}

		private void SaveAppSettingsMethod()
		{
			if (_mainPage?.FilterPage?.FilterModel == null || !SaveSettings)
				return;
			var currentFilterModel = _mainPage.FilterPage.FilterModel;

			var settings = new FilterModel
			{
				City = currentFilterModel.City,
				Country = currentFilterModel.Country.Where(k => k.Value.IsChecked).ToDictionary(o => o.Key, o => o.Value),
				CurrentMaxSpeed = currentFilterModel.CurrentMaxSpeed,
				CurrentMaxUptime = currentFilterModel.CurrentMaxUptime,
				CurrentMinSpeed = currentFilterModel.CurrentMinSpeed,
				CurrentMinUptime = currentFilterModel.CurrentMinUptime,
				MaxSpeed = currentFilterModel.MaxSpeed,
				MaxUptime = currentFilterModel.MaxUptime,
				MinSpeed = currentFilterModel.MinSpeed,
				MinUptime = currentFilterModel.MinUptime
			};
			var setting = JsonConvert.SerializeObject(settings);
			File.WriteAllText(_settingFileName, setting);
		}

		private async void LoadAppSettingsMethod()
		{
			if (_mainPage?.FilterPage?.FilterModel == null || _mainPage.FilterPage.FilterModel.Country.Count <= 0 || !SaveSettings)
				return;
			var currentFilterModel = _mainPage.FilterPage.FilterModel;
			try
			{
				var file = File.ReadAllText(_settingFileName);
				var settings = JsonConvert.DeserializeObject<FilterModel>(file);
				if (settings.Country.Count <= 0)
					return;
				currentFilterModel.City = settings.City;

				if (currentFilterModel.CurrentMaxSpeed >= settings.MaxSpeed)
					currentFilterModel.MaxSpeed = settings.MaxSpeed;
				if (currentFilterModel.CurrentMinSpeed <= settings.MinSpeed)
					currentFilterModel.MinSpeed = settings.MinSpeed;

				if (currentFilterModel.CurrentMaxUptime >= settings.MaxUptime)
					currentFilterModel.MaxUptime = settings.MaxUptime;
				if (currentFilterModel.CurrentMinUptime <= settings.MinUptime)
					currentFilterModel.MinUptime = settings.MinUptime;

				//var сommonСountries = currentFilterModel.Country.Intersect(settings.Country.Keys)
				//.ToDictionary(a => a.Key, b => b.Value).Where(v => v.Value.IsChecked);
				var сommonСountries = currentFilterModel.Country.Keys.Intersect(settings.Country.Keys).ToArray();
				if (сommonСountries.Length > 0)
				{
					foreach (var country in currentFilterModel.Country)
					{
						country.Value.IsChecked = сommonСountries.Contains(country.Key);
					}
				}

				await _mainPage.DialogPage.ExecuteFilter();
			}
			catch (Exception)
			{
				//ignore
				//MessageBox.Show(exception.ToString());
			}

			//var settings = new FilterModel
			//   {
			//    City = currentFilterModel.City,
			//    Country = currentFilterModel.Country.Where(k => k.Value.IsChecked).ToDictionary(o => o.Key, o => o.Value),
			//    CurrentMaxSpeed = currentFilterModel.CurrentMaxSpeed,
			//    CurrentMaxUptime = currentFilterModel.CurrentMaxUptime,
			//    CurrentMinSpeed = currentFilterModel.CurrentMinSpeed,
			//    CurrentMinUptime = currentFilterModel.CurrentMinUptime,
			//    MaxSpeed = currentFilterModel.MaxSpeed,
			//    MaxUptime = currentFilterModel.MaxUptime,
			//    MinSpeed = currentFilterModel.MinSpeed,
			//    MinUptime = currentFilterModel.MinUptime,
			//   };
			//   JsonConvert.SerializeObject<FilterModel>(response)
			//MainPage.FilterPage.
		}
	}
}