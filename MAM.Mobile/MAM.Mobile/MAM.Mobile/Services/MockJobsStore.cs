using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAM.Mobile.Models;
using MAM.Mobile.Views;

namespace MAM.Mobile.Services
{
    public class MockJobsStore : IDataStore<JobsMenuItem>
    {
        List<JobsMenuItem> items;

        public MockJobsStore()
        {
            items = new List<JobsMenuItem>();
            var mockItems = new List<JobsMenuItem>
            {
                new JobsMenuItem { Id =Guid.NewGuid().ToString(), Title = "First item", },
                new JobsMenuItem { Id = Guid.NewGuid().ToString(), Title = "Second item" },

            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(JobsMenuItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(JobsMenuItem item)
        {
            var oldItem = items.Where((JobsMenuItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((JobsMenuItem arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<JobsMenuItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<JobsMenuItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}