using Refit;
using SkeduloTest.Models;
using SkeduloTest.Refits;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace SkeduloTest.Services
{
    public class BreedService : IBreedService
    {
        private readonly IBreedData _breedData;
        public BreedService()
        {
            _breedData = RestService.For<IBreedData>(DependencyService.Get<HttpClient>());
        }
        public async Task<IEnumerable<Breed>> GetAllBreeds()
        {
            return await _breedData.GetAll();
        }

        public async Task<IEnumerable<Breed>> GetByPage(int page)
        {
            return await _breedData.GetByPage(page.ToString());
        }
    }
}
