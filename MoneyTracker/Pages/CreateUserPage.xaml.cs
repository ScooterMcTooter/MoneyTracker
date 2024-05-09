using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class CreateUserPage : ContentPage
{
	public CreateUserPage(UserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}