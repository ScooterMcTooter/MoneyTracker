using MoneyTracker.Pages;
using MoneyTracker.ViewModels;

namespace MoneyTracker; 

public partial class AppShell : Shell
{
    private readonly SettingsViewModel _vm;
    public AppShell(SettingsViewModel vm)
    {
        InitializeComponent();

        BindingContext = _vm = vm;

        Routing.RegisterRoute(nameof(PasswordResetPage), typeof(PasswordResetPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        Routing.RegisterRoute(nameof(TransactionsPage), typeof(TransactionsPage));
    }
}
