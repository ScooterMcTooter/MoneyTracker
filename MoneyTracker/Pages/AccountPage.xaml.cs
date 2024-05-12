using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage(AccountViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}