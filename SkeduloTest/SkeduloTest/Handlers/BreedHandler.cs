using SkeduloTest.ContentViews;
using SkeduloTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace SkeduloTest.Handlers
{
    public class BreedHandler : SearchHandler
    {

        public static readonly BindableProperty BreedsProperty = BindableProperty.Create(nameof(Breeds),
                                                                                typeof(ObservableCollection<Breed>),
                                                                                typeof(BreedHandler),
                                                                                new ObservableCollection<Breed>(),
                                                                                BindingMode.OneWay);
        public ObservableCollection<Breed> Breeds
        {
            get => (ObservableCollection<Breed>)GetValue(BreedsProperty);
            set => SetValue(BreedsProperty, value);
        }
        public BreedHandler()
        {
            Breeds = new ObservableCollection<Breed>();
        }
        public string NavigationRoute { get; set; }
        public Type NavigationType { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Breeds.Where(breed => breed.name.ToLower().Contains(newValue.ToLower())).ToList();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            //var navParam = new Dictionary<string, object>();
            //navParam.Add("StudentDetail", item);
            //if (!string.IsNullOrWhiteSpace(NavigationRoute))
            //{
            //    await Shell.Current.GoToAsync(NavigationRoute, navParam);
            //}
        }
    }
}
