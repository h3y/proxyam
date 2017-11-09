using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace proxyam.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        public ICommand SelectLanguageCommand { get; private set; }
        private MainViewModel MainPage;

        public SettingViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            SelectLanguageCommand = new RelayCommand<ComboBoxItem>(Execute);
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
    }
}