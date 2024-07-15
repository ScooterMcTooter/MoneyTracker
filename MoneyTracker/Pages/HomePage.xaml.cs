using CommunityToolkit.Mvvm.Messaging;
using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class HomePage : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public HomePage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        InitializeComponent();
        BindingContext = _serviceProvider.GetService<HomeViewModel>();

        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;

        WeakReferenceMessenger.Default.Register<ScrollToMessage>(this, async (r, m) =>
        {
            await HomeScroll.ScrollToAsync(QuickAddFrame, ScrollToPosition.Start, true);
        });

        WeakReferenceMessenger.Default.Register<ScrollToTop>(this, async (r, m) =>
        {
            await HomeScroll.ScrollToAsync(0, 0, true);
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoanListView.SelectedItem = null;
    }

    private void HomePageBind_Appearing(object sender, EventArgs e)
    {
        var existingPages = Shell.Current.Navigation.NavigationStack.ToList();
        foreach (var page in existingPages)
        {
            if (page != null && page != this)
            {
                Shell.Current.Navigation.RemovePage(page);
            }
        }
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem is MoneyTrackerMigrations.Models.LoanModel loan)
            {

                _serviceProvider.GetService<LoanViewModel>().EditLoanCommand.Execute(Constants.ConstLoans.Where(l => l.Id == loan.Id).First().Id);
            }
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }
}