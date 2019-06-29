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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = loginViewModel = new LoginViewModel();

        }

        void Save_Clicked(object sender, EventArgs e)
        {
            Utility.Role = "Producer";

            if (loginViewModel.UserName.ToUpper() == "PRAGEETH")
            {
                Utility.Role = "Consumer";
            }

            Application.Current.MainPage = new MainPage();
        }

        void Cancel_Clicked(object sender, EventArgs e)
        {
            loginViewModel.UserName = "";
            loginViewModel.PW = "";
        }
    }
}