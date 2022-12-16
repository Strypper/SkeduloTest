using SkeduloTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkeduloTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BreedPage : ContentPage
    {
        BreedViewModel _viewModel;

        public BreedPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new BreedViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void BreedHandler_SelectBreed(Models.Breed breed)
        {
            BreedCollectionView.ScrollTo(breed, animate: true);
        }
    }
}