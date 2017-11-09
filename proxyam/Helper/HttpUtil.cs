using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using proxyam.ViewModel;

namespace proxyam.Helper
{
    public class HttpUtil
    {
        public readonly MainViewModel MainPage;

        public HttpUtil(MainViewModel mainPage)
        {
            MainPage = mainPage;
        }

        public async Task<String> MakeRequestAsync(string url, double delay = 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            String responseText = await Task.Run(() =>
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    WebResponse response = request?.GetResponse();
                    Stream responseStream = response?.GetResponseStream();
                    return new StreamReader(responseStream ?? throw new WebException()).ReadToEnd();
                }
                catch (WebException ex)
                {
                    switch (ex.Status)
                    {
                        case WebExceptionStatus.UnknownError:
                            MainPage.HttpUtil.ShowErrorWindow("UnknownError");
                            break;

                        case WebExceptionStatus.ProtocolError:
                            MainPage.HttpUtil.ShowErrorWindow("ProtocolError", ((ex.Response as HttpWebResponse)?.StatusCode).ToString());
                            break;

                        case WebExceptionStatus.ConnectFailure:
                            MainPage.HttpUtil.ShowErrorWindow("ConnectFailure");
                            break;

                        case WebExceptionStatus.SendFailure:
                            MainPage.HttpUtil.ShowErrorWindow("SendFailure");
                            break;

                        case WebExceptionStatus.ReceiveFailure:
                            MainPage.HttpUtil.ShowErrorWindow("ReceiveFailure");
                            break;
                    }
                }
                return null;
            });
            return responseText;
        }
        public void ShowErrorWindow(string message, string additionalMessage = "")
        {
            MainPage.ErrorModalPage.ShowErrorModalCommand.Execute(
                MainPage.GetLanguageMessage(message) + additionalMessage);
        }
    }
}
