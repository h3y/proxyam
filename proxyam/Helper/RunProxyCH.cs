using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace proxyam.Helper
{
	static class RunProxyCh
	{
		[DllImport("wininet.dll")]
		private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
		private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
		private const int INTERNET_OPTION_REFRESH = 37;

		public static void Start(string url)
		{
			string proxy = url;
			//switch (proxyType)
			//{
			//	case "SOCKS":
			//		proxy = "socks=" + url; break;
			//	case "HTTP(S)":
			//		proxy = url; break;
			//}

			RegistryKey regKey = Registry.CurrentUser;
			regKey = regKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
			regKey.SetValue("ProxyEnable", 1);
			regKey.SetValue("ProxyServer", proxy);
			regKey.Close();
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
			ProcessStartInfo psi = new ProcessStartInfo(Environment.SystemDirectory + @"\netsh.exe", "winhttp import proxy source=ie");
			psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			Process.Start(psi);
		}
		public static void Stop()
		{
			RegistryKey regKey = Registry.CurrentUser;
			regKey = regKey.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
			regKey.SetValue("ProxyEnable", 0);
			regKey.Close();
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
			ProcessStartInfo psi = new ProcessStartInfo(Environment.SystemDirectory + @"\netsh.exe", "winhttp import proxy source=ie");
			psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			Process.Start(psi);
		}
	}
}
