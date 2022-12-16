using Refit;
using SkeduloTest.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkeduloTest.Refits
{
    public interface IBreedData
    {
        [Get("/breeds")]
        Task<IEnumerable<Breed>> GetAll();

        [Get("/breeds?limit=5&page={page}")]
        Task<IEnumerable<Breed>> GetByPage(string page);
    }
}
