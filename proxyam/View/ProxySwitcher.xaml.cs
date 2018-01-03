using proxyam.Model;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace proxyam.View
{
    /// <summary>
    /// Логика взаимодействия для ProxySwitcher.xaml
    /// </summary>
    public partial class ProxySwitcher : UserControl
    {

        //[DllImport(@"proxyam86.dll", EntryPoint = "startProxy")]
        //public static extern int startProxy(string url);

        //[DllImport(@"proxyam86.dll", EntryPoint = "stopProxy")]
        //public static extern int stopProxy();

        //bool isEnable = false;
        public ProxySwitcher()
        {
            InitializeComponent();
        }

        //private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    //if (isEnable)
        //    //{
        //    //    stopProxy();
        //    //}
        //    //int code = startProxy($"https://{((sender as DataGrid).SelectedItem as Proxy).Proxies}");
        //    //isEnable = true;
        //}
    }
}
