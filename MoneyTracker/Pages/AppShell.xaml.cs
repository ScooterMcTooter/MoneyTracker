using MoneyTracker.Pages;
using MoneyTracker.ViewModels;

namespace MoneyTracker; 

public partial class AppShell : Shell
{
    private readonly IServiceProvider _serviceProvider;
    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        BindingContext = _serviceProvider.GetService<SettingsViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the SettingsViewModel service.");

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(PasswordResetPage), typeof(PasswordResetPage));
        Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        Routing.RegisterRoute(nameof(TransactionsPage), typeof(TransactionsPage));
        Routing.RegisterRoute(nameof(JobPage), typeof(JobPage));
        Routing.RegisterRoute(nameof(LoanPage), typeof(LoanPage));
    }
}
