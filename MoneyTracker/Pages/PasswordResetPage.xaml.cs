using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class PasswordResetPage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;
    public PasswordResetPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new PasswordResetViewModel(_serviceProvider.GetService<ApplicationDbContext>());
    }
}
