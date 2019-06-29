using MAM.Mobile.Services;
using MAM.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//help

namespace MAM.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsMaster : ContentPage
    {
        public ListView ListView;
        JobsMasterViewModel viewModel;

        public JobsMaster()
        {
            InitializeComponent();

            BindingContext = viewModel = new JobsMasterViewModel();
            ListView = MenuItemsListView;
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as JobsMenuItem;
            if (item == null)
                return;
           


            await Navigation.PushModalAsync(new NavigationPage(new AJob(new JobDetailViewModel(item))));

            // Manually deselect item.
            ListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.MenuItems.Count == 0)
                viewModel.LoadJobsCommand.Execute(null);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new JobsDetail()));
        }

        public class JobsMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<JobsMenuItem> MenuItems { get; set; }
            public IDataStore<JobsMenuItem> DataStore => DependencyService.Get<IDataStore<JobsMenuItem>>() ?? new MockJobsStore();
            public Command LoadJobsCommand { get; set; }
            bool isBusy = false;
            public bool IsBusy
            {
                get { return isBusy; }
                set { SetProperty(ref isBusy, value); }
            }

            public JobsMasterViewModel()
            {
                MenuItems = new ObservableCollection<JobsMenuItem>();
                LoadJobsCommand = new Command(async () => await ExecuteLoadJobsCommand());

                MessagingCenter.Subscribe<JobsDetail, JobsMenuItem>(this, "AddItem", async (obj, item) =>
                {
                    var newItem = item as JobsMenuItem;
                    MenuItems.Add(newItem);
                    await DataStore.AddItemAsync(newItem);
                });

                MessagingCenter.Subscribe<AJob, JobsMenuItem>(this, "UpdateItem", async (obj, item) =>
                {
                    var newItem = item as JobsMenuItem;
                    MenuItems.Remove(MenuItems.FirstOrDefault(s => s.Id == newItem.Id));
                    MenuItems.Add(newItem);
                    await DataStore.UpdateItemAsync(newItem);
                });
            }

            public async Task ExecuteLoadJobsCommand()
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                try
                {
                    MenuItems.Clear();
                    var items = await DataStore.GetItemsAsync(false);
                    foreach (var item in items)
                    {
                        MenuItems.Add(item);
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
            protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
            {
                if (EqualityComparer<T>.Default.Equals(backingStore, value))
                    return false;

                backingStore = value;
                onChanged?.Invoke();
                OnPropertyChanged(propertyName);
                return true;
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}