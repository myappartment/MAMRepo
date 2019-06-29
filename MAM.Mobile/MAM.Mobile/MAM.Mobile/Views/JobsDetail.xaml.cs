using MAM.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MAM.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsDetail : ContentPage
    {
        JobDetailViewModel jobsDetailViewModel;
        public JobsMenuItem Item { get; set; }


        public JobsDetail(JobDetailViewModel jobsDetailViewModel)
        {
            InitializeComponent();
            Item = new JobsMenuItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = "This is an item description."
            };

            BindingContext = this;

            //BindingContext = this.jobsDetailViewModel = jobsDetailViewModel;
        }

        public JobsDetail()
        {
            InitializeComponent();
            Item = new JobsMenuItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = "This is an item description."
            };

            BindingContext = jobsDetailViewModel = new JobDetailViewModel(Item);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", jobsDetailViewModel.Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}