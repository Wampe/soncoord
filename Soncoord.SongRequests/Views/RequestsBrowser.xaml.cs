using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Soncoord.SongRequests.Views
{
    public partial class RequestsBrowser : UserControl
    {
        public RequestsBrowser(IStreamerSonglistService providerService)
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            //var webView2 = new WebView2();
            // must create a data folder if running out of a secured folder that can't write like Program Files
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}\\Soncoord";
            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: path);

            // NOTE: this waits until the first page is navigated - then continues
            //       executing the next line of code!
            await webView.EnsureCoreWebView2Async(env);

            //if (Model.Options.AutoOpenDevTools)
            webView.CoreWebView2.OpenDevToolsWindow();

            // Almost always need this event for something    
            webView.NavigationCompleted += WebView_NavigationCompleted;

            // set the initial URL
            webView.Source = new Uri("https://www.streamersonglist.com");

            
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //if (e.IsSuccess)
            //    Model.Url = webView.Source.ToString();

            //if (firstload)
            //{
            //    firstload = false;
            //    webView.Visibility = Visibility.Visible;
            //}
            Call();
        }

        private async void Call()
        {
            

            const string tokenItem = "localStorage.getItem('StreamerSonglist_authToken');";
            var tokenResult = await webView.ExecuteScriptAsync(tokenItem);
            if (tokenResult != "null")
            {
                var token = JsonConvert.DeserializeObject<string>(JsonConvert.DeserializeObject<string>(tokenResult));
                Console.WriteLine(token);
            }

            const string userItem = "localStorage.getItem('StreamerSonglist_user');";
            var userResult = await webView.ExecuteScriptAsync(userItem);
            if (userResult != "null")
            {
                var user = JsonConvert.DeserializeObject<StreamerSonglistUser>(JsonConvert.DeserializeObject<string>(userResult));
                Console.WriteLine(user.Username);
            }

            // Json Result "\"...\""
            // !!! _> Parse Json To Object and then remove the " (value from local storage)
            //Console.WriteLine(scriptResult);
            //char[] charsToTrim = { '"' };
            //Console.WriteLine(token.Trim(charsToTrim));
            //Console.WriteLine(JsonConvert.DeserializeObject<string>(token));

            // ToDo: Save to setting file (?)

            //MessageBox.Show(this, scriptResult, "Script Result");
        }
    }
}
