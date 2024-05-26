using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}