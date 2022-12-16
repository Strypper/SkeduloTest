using SkeduloTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeduloTest.Services
{
    public interface IBreedService
    {
        Task<IEnumerable<Breed>> GetAllBreeds();

        Task<IEnumerable<Breed>> GetByPage(int page);
    }
}
