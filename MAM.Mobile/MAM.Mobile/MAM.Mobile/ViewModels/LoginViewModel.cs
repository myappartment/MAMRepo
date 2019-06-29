namespace MAM.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel// INotifyPropertyChanged
    {
        string username = string.Empty;
        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        string pw = string.Empty;
        public string PW
        {
            get { return pw; }
            set { SetProperty(ref pw, value); }
        }
    }
}
