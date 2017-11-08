using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace proxyam.ViewModel
{
    public class ErrorModalViewModel : ObservableObject
    {
        public ICommand ShowErrorModalCommand { get; private set; }
        public MainViewModel MainPage { get; }
        private string _errorText;
        private Boolean _showErrorModal;

        public ErrorModalViewModel(MainViewModel mainPage)
        {
            MainPage = mainPage;
            ShowErrorModalCommand = new RelayCommand<string>(ExecuteShowErrorModal);
        }

        private void ExecuteShowErrorModal(string text)
        {
            Task.Run(() =>
            {
                ErrorText = text;
                ShowErrorModal = true;
                Thread.Sleep(2000);
                ShowErrorModal = false;
            });
        }

        public string ErrorText
        {
            get => _errorText;
            set => Set(() => ErrorText, ref _errorText, value);
        }

        public Boolean ShowErrorModal
        {
            get => _showErrorModal;
            set => Set(() => ShowErrorModal, ref _showErrorModal, value);
        }
    }
}