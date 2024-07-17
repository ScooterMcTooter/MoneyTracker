using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class LoanPage : ContentPage
{
	private readonly IServiceProvider _serviceProvider;

	public LoanPage(IServiceProvider serviceProvider)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
		BindingContext = _serviceProvider.GetService<LoanViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the LoanView service.");
	}
}