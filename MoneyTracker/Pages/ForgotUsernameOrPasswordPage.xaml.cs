using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class ForgotUsernameOrPasswordPage : ContentPage
{
	public ForgotUsernameOrPasswordPage(UserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}