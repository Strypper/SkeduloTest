using Newtonsoft.Json;
using SkeduloTest.Models;
using SkeduloTest.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static System.Net.Mime.MediaTypeNames;

namespace SkeduloTest.ViewModels
{
    public class BreedViewModel : BaseViewModel
    {
        private IBreedService breedService;
        private int page = 1;

        public NetworkAccess InternetStatus;
        public ObservableCollection<Breed> Breeds { get; private set; }
        public Command LoadBreedsCommand { get; }
        public Command IncreamentLoadCommand { get; }
        public BreedViewModel()
        {
            ItemThreshold = 1;
            Title = "Breed Page";
            Breeds = new ObservableCollection<Breed>();
            InternetStatus = Connectivity.NetworkAccess;

            LoadBreedsCommand = new Command(async () => await ExecuteLoadBreedsCommand());
            IncreamentLoadCommand = new Command(async () => await ItemsTresholdReached());

            breedService = DependencyService.Get<IBreedService>();
        }

        async Task ExecuteLoadBreedsCommand()
        {
            IsBusy = true;

            if(InternetStatus == NetworkAccess.Internet)
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
                GetLocalData().ForEach(breed => Breeds.Add(breed));
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

        private void SaveToLocal(IEnumerable<Breed> breeds)
        {
            File.WriteAllText("LocalFile.txt", JsonConvert.SerializeObject(breeds));
        }

        private IEnumerable<Breed> GetLocalData()
        {
            //if(File.ReadAllText("LocalFile.txt") != null)
            //{
            //    string localData = File.ReadAllText("LocalFile.txt");
            //    return JsonConvert.DeserializeObject<List<Breed>>(localData);
            //}
            //return new List<Breed>();
            try
            {
                string localData = File.ReadAllText("LocalFile.txt");
                return JsonConvert.DeserializeObject<List<Breed>>(localData);
            }
            catch (FileNotFoundException e)
            {
                return new List<Breed>();
            }
        }
    }
}
