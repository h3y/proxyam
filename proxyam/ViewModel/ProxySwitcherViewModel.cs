using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace proxyam.ViewModel
{
    public class ProxySwitcherViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public ProxySwitcherViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
    }
}
