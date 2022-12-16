using SkeduloTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkeduloTest.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Consume thecatapi.com Gateway", Description="I was struggle the docs little bit, but found a way to implement", IsDone = true},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Show breeds", Description="Extracted Component. Please explore \nBreed.cs\nContentViews/BreedCardContentView.xaml", IsDone = true },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Flexible image size", Description="The image of the breed should scale for being able to show a full image without\r\ncropping the image. Which means each item can have different sizes.", IsDone = true },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Check internet", Description="Done. Simple implementation \nViewModel/BreedViewModel.cs", IsDone = true },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Local Database", Description="Use Sqlite to save data, I don't know why I can't save all the original tables, I'm come from the EF Core and SQL Server so this feel a little bit weird. \nAnyway I have gather all the models in one place and store them", IsDone=true }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}