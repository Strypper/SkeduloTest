using SkeduloTest.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkeduloTest.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BreedCardContentView : ContentView
    {
        public static readonly BindableProperty BreedProperty = BindableProperty.Create(nameof(Breed),
                                                                                        typeof(Breed),
                                                                                        typeof(BreedCardContentView),
                                                                                        default(Breed));
        public Breed Breed
        {
            get => (Breed)GetValue(BreedProperty);
            set => SetValue(BreedProperty, value);
        }

        public BreedCardContentView()
        {
            InitializeComponent();
        }
    }
}