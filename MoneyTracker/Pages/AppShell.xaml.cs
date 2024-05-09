using MoneyTracker.Pages;

namespace MoneyTracker; 

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ForgotUsernameOrPasswordPage), typeof(ForgotUsernameOrPasswordPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
    }
}
