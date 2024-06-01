using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class LoanPage : ContentPage
{
	public LoanPage(LoanViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}