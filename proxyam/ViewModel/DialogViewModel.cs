using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using proxyam.View;

namespace proxyam.ViewModel
{
    public class DialogViewModel
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
                    _template = MainPage.SettingPage; break;
                case "Filter":
                    _template = MainPage.FilterPage; break;
            }
            //show the dialog
            var result = await DialogHost.Show(_template, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SplashScreen());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
