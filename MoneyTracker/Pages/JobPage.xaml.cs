using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class JobPage : ContentPage
{
	public JobPage(JobViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}