using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using Soncoord.Infrastructure;
using Soncoord.Infrastructure.Interfaces.Services;
using Soncoord.Infrastructure.Models;
using System;
using System.Windows.Controls;

namespace Soncoord.SongRequests.Views
{
    public partial class RequestsBrowser : UserControl
    {
        private readonly IStreamerSonglistService _providerService;

        public RequestsBrowser(IStreamerSonglistService providerService)
        { 
            _providerService = providerService;
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            var environment = await CoreWebView2Environment.CreateAsync(userDataFolder: Globals.TemporaryNetCache);
            await webView.EnsureCoreWebView2Async(environment);
            webView.NavigationCompleted += NavigationCompleted;
            webView.Source = new Uri("https://www.streamersonglist.com");
        }

        private void NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            Call();
        }

        private async void Call()
        {
            const string tokenItem = "localStorage.getItem('StreamerSonglist_authToken');";
            var tokenResult = await webView.ExecuteScriptAsync(tokenItem);
            var token = JsonConvert.DeserializeObject<string>(JsonConvert.DeserializeObject<string>(tokenResult));

            const string userItem = "localStorage.getItem('StreamerSonglist_user');";
            var userResult = await webView.ExecuteScriptAsync(userItem);
            var user = JsonConvert.DeserializeObject<StreamerSonglistUser>(JsonConvert.DeserializeObject<string>(userResult));

            _providerService.SetUser(user, token);
        }
    }
}
