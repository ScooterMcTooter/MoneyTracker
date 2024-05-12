using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class TransactionsPage : ContentPage
{
	public TransactionsPage(TransactionsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}