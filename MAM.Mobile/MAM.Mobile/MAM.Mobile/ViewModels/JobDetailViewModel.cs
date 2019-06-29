using MAM.Mobile.Services;
using MAM.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MAM.Mobile.ViewModels
{
    public class JobDetailViewModel : INotifyPropertyChanged
    {
            public IDataStore<JobsMenuItem> DataStore => DependencyService.Get<IDataStore<JobsMenuItem>>() ?? new MockJobsStore();

        public int MyProperty { get; set; }
        public JobsMenuItem Item { get; set; }

        public JobDetailViewModel(JobsMenuItem Item)
        {
            this.Item = Item;
        }
        
        public string Title { get; set; }
        public string JobDescription { get; set; }

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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
