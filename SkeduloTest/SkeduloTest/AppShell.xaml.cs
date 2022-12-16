using SkeduloTest.ViewModels;
using SkeduloTest.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SkeduloTest
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(BreedPage), typeof(BreedPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
