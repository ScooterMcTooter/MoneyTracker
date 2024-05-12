using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class CreateUserPage : ContentPage
{
	public CreateUserPage(CreateUserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}