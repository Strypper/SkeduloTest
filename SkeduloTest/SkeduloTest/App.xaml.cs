using SkeduloTest.Handlers;
using SkeduloTest.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace SkeduloTest
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var apiKey = "live_HWJY5fHxnRBCLIYCXNLnz9YLoOENoH9NdYDkDAMWVahrF4UoniRpHzbqedvCw0bm";
            DependencyService.Register<MockDataStore>();

            var catApiHttpClient = new HttpClient();

            catApiHttpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
            catApiHttpClient.BaseAddress = new Uri("https://api.thecatapi.com/v1");
            DependencyService.RegisterSingleton<HttpClient>(catApiHttpClient);

            DependencyService.Register<BreedService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
