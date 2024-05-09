using MoneyTracker.ViewModels;

namespace MoneyTracker
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(UserViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
