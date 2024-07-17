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
        BindingContext = _serviceProvider.GetService<HomeViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the HomeView service."); ;

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

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var list = sender as ListView;
            if (list != null)
            {
                LoanModel selectedLoan = list.SelectedItem as LoanModel;
                if (selectedLoan != null)
                {
                    _serviceProvider.GetService<LoanViewModel>().SelectedLoan = selectedLoan;
                    await Shell.Current.GoToAsync(nameof(LoanPage));
                }
                await Shell.Current.GoToAsync($"{nameof(LoanPage)}");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }
}