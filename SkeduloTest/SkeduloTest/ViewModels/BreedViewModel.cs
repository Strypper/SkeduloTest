using SkeduloTest.Models;
using SkeduloTest.Services;
using SkeduloTest.SqliteModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SkeduloTest.ViewModels
{
    public class BreedViewModel : BaseViewModel
    {
        private IBreedService breedService;
        private int page = 1;
        private SQLiteAsyncConnection _dbConnection;

        public NetworkAccess InternetStatus;
        public ObservableCollection<Breed> Breeds { get; private set; }
        public Command LoadBreedsCommand { get; }
        public Command IncreamentLoadCommand { get; }

        public Command SaveToCacheCommand { get; }
        public BreedViewModel()
        {
            ItemThreshold = 1;
            Title = "Breed Page";
            Breeds = new ObservableCollection<Breed>();
            InternetStatus = Connectivity.NetworkAccess;

            SaveToCacheCommand = new Command<IEnumerable<Breed>>((IEnumerable<Breed> breeds) => SaveToLocal(breeds));
            LoadBreedsCommand = new Command(async () => await ExecuteLoadBreedsCommand());
            IncreamentLoadCommand = new Command(async () => await ItemsTresholdReached());

            breedService = DependencyService.Get<IBreedService>();
        }

        async Task ExecuteLoadBreedsCommand()
        {
            IsBusy = true;

            if (InternetStatus == NetworkAccess.Internet)
            {
                try
                {
                    ItemThreshold = 1;
                    var breeds = await breedService.GetByPage(page);
                    foreach (var breed in breeds)
                    {
                        Breeds.Add(breed);
                    }
                    SaveToLocal(breeds);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                //Handle Offline
                await GetLocalData();
                IsBusy = false;
            }
        }

        async Task ItemsTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            page++;
            if (InternetStatus != NetworkAccess.None)
            {
                try
                {
                    var items = await breedService.GetByPage(page);
                    foreach (var item in items)
                    {
                        Breeds.Add(item);
                    }
                    if (items.Any())
                    {
                        ItemThreshold = -1;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void SaveToLocal(IEnumerable<Breed> breeds)
        {
            await SetUpDb();
            foreach (var breed in breeds)
            {
                await _dbConnection.InsertAsync(new LocalStorageModel()
                {
                    id = breed.id,
                    name = breed.name,
                    cfa_url = breed.cfa_url,
                    vetstreet_url = breed.vetstreet_url,
                    vcahospitals_url = breed.vcahospitals_url,
                    temperament = breed.temperament,
                    origin = breed.origin,
                    country_codes = breed.country_codes,
                    country_code = breed.country_code,
                    description = breed.description,
                    life_span = breed.life_span,
                    indoor = breed.indoor,
                    lap = breed.lap,
                    alt_names = breed.alt_names,
                    adaptability = breed.adaptability,
                    affection_level = breed.affection_level,
                    child_friendly = breed.child_friendly,
                    dog_friendly = breed.dog_friendly,
                    energy_level = breed.energy_level,
                    grooming = breed.grooming,
                    health_issues = breed.health_issues,
                    intelligence = breed.intelligence,
                    shedding_level = breed.shedding_level,
                    social_needs = breed.social_needs,
                    stranger_friendly = breed.stranger_friendly,
                    vocalisation = breed.vocalisation,
                    experimental = breed.experimental,
                    hairless = breed.hairless,
                    natural = breed.natural,
                    rare = breed.rare,
                    rex = breed.rex,
                    suppressed_tail = breed.suppressed_tail,
                    short_legs = breed.short_legs,
                    wikipedia_url = breed.wikipedia_url,
                    hypoallergenic = breed.hypoallergenic,
                    reference_image_id = breed.reference_image_id,
                    cat_friendly = breed.cat_friendly,
                    bidability = breed.bidability,
                    image_id = breed.image.id,
                    width = breed.image.width,
                    height = breed.image.height,
                    url = breed.image.url,
                    imperial = breed.weight.imperial,
                    metric = breed.weight.metric
                });
            }
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Store local success", "Breeds are ready when offline", "Ok");
        }

        //await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Can't get local file", e.Message, "Ok");
        private async Task GetLocalData()
        {
            await SetUpDb();

            var localData = await _dbConnection.Table<LocalStorageModel>().ToListAsync();
            localData.ForEach(data => Breeds.Add(new Breed()
            {
                id = data.id,
                name = data.name,
                cfa_url = data.cfa_url,
                vetstreet_url = data.vetstreet_url,
                vcahospitals_url = data.vcahospitals_url,
                temperament = data.temperament,
                origin = data.origin,
                country_codes = data.country_codes,
                country_code = data.country_code,
                description = data.description,
                life_span = data.life_span,
                indoor = data.indoor,
                lap = data.lap,
                alt_names = data.alt_names,
                adaptability = data.adaptability,
                affection_level = data.affection_level,
                child_friendly = data.child_friendly,
                dog_friendly = data.dog_friendly,
                energy_level = data.energy_level,
                grooming = data.grooming,
                health_issues = data.health_issues,
                intelligence = data.intelligence,
                shedding_level = data.shedding_level,
                social_needs = data.social_needs,
                stranger_friendly = data.stranger_friendly,
                vocalisation = data.vocalisation,
                experimental = data.experimental,
                hairless = data.hairless,
                natural = data.natural,
                rare = data.rare,
                rex = data.rex,
                suppressed_tail = data.suppressed_tail,
                short_legs = data.short_legs,
                wikipedia_url = data.wikipedia_url,
                hypoallergenic = data.hypoallergenic,
                reference_image_id = data.reference_image_id,
                cat_friendly = data.cat_friendly,
                bidability = data.bidability,
                image = new Models.Image()
                {
                    id = data.image_id,
                    width = data.width,
                    height = data.height,
                    url = data.url
                },
                weight = new Weight()
                {
                    imperial = data.imperial,
                    metric = data.metric
                }
            }));
        }

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Skedulo.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<LocalStorageModel>();
            }
        }
    }
}
