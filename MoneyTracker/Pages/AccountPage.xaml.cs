using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class AccountPage : ContentPage
{
	public AccountPage(UserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}