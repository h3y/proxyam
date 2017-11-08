using System.Windows;
using GalaSoft.MvvmLight;

namespace proxyam.ViewModel
{
    public class SplashScreenViewModel : ObservableObject
    {
        private Visibility _showContent = Visibility.Visible;
        private Visibility _showSplash = Visibility.Hidden;

        public Visibility ShowContent
        {
            get => _showContent;
            set => Set(() => ShowContent, ref _showContent, value);
        }

        public Visibility ShowSplash
        {
            get => _showSplash;
            set { Set(() => ShowSplash, ref _showSplash, value); }
        }

        public void SetShowSplash(bool show)
        {
            if (show)
            {
                ShowSplash = Visibility.Visible;
                ShowContent = Visibility.Hidden;
            }
            else
            {
                ShowSplash = Visibility.Hidden;
                ShowContent = Visibility.Visible;
            }
        }
    }
}