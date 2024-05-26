using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel? vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void Password_Entered(object sender, EventArgs e)
    {
        if (LoginButton.Command?.CanExecute(null) == true)
        {
            // Execute the LoginCommand
            LoginButton.Command.Execute(null);
        }
    }
}
