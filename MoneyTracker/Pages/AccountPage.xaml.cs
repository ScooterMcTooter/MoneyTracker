using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class AccountPage : ContentPage
{
	private readonly IServiceProvider _serviceProvider;

	public AccountPage(IServiceProvider serviceProvider)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
		BindingContext = _serviceProvider.GetService<AccountViewModel>();
	}
}