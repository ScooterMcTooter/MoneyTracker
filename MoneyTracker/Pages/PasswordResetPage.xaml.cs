using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class PasswordResetPage : ContentPage
{
	public PasswordResetPage(PasswordResetViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}