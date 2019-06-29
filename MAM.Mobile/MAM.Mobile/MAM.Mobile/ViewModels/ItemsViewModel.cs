using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MAM.Mobile.Models;
using MAM.Mobile.Views;
using MAM.Mobile.Services;

namespace MAM.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<JobsMenuItem> JobItems { get; set; }
        public IDataStore<JobsMenuItem> DataStoreJobs => DependencyService.Get<IDataStore<JobsMenuItem>>() ?? new MockJobsStore();


        public Command LoadItemsCommand { get; set; }
        public Command LoadJobsCommand { get; set; }


        public ItemsViewModel()
        {
            if (Utility.Role == "Producer")
            {
                Title = "Browse";
                Items = new ObservableCollection<Item>();
                LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

                MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
                {
                    var newItem = item as Item;
                    Items.Add(newItem);
                    await DataStore.AddItemAsync(newItem);
                });
            }
            else
            {
                Title = "Jobs";
                JobItems = new ObservableCollection<JobsMenuItem>();
                LoadJobsCommand = new Command(async () => await ExecuteLoadJobsCommand());

                MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
                {
                    var newItem = item as Item;
                    Items.Add(newItem);
                    await DataStore.AddItemAsync(newItem);
                });
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public async Task ExecuteLoadJobsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                JobItems.Clear();
                var items = await DataStoreJobs.GetItemsAsync(false);
                foreach (var item in items)
                {
                    JobItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}