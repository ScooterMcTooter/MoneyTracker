using CommunityToolkit.Mvvm.Messaging;
using MoneyTracker.ViewModels;

namespace MoneyTracker.Pages;

public partial class HomePage : ContentPage
{
    private readonly IServiceProvider _serviceProvider = null!;
    private readonly AccountViewModel _AccountViewModel = null!;
    private readonly LoanViewModel _LoanViewModel = null!;
    private readonly JobViewModel _JobViewModel = null!;

    public HomePage(IServiceProvider serviceProvider)
    {
        try
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();
            BindingContext = _serviceProvider.GetService<HomeViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the HomeView service.");
            _AccountViewModel = _serviceProvider.GetService<AccountViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the AccountViewModel service.");
            _LoanViewModel = _serviceProvider.GetService<LoanViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the LoanViewModel service.");
            _JobViewModel = _serviceProvider.GetService<JobViewModel>() ?? throw new NotImplementedException("There is a failure when trying to access the JobViewModel service.");

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
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("error", ex.ToString(), "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Clear selection for LoanListView
        LoanListView.SelectedItem = null;

        // Clear selection for AccountListView
        AccountListView.SelectedItem = null;

        // Clear selection for JobListView
        JobListView.SelectedItem = null;

        // Add similar lines for any other ListView controls you have
        // Example:
        // AnotherListView.SelectedItem = null;
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

    private async void ListView_LoanSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var list = sender as ListView;
            if (list != null)
            {
                LoanModel? selectedLoan = list.SelectedItem as LoanModel;
                if (selectedLoan != null)
                {
                    _LoanViewModel.SelectedLoan = selectedLoan;
                    await Shell.Current.GoToAsync($"//{nameof(LoanPage)}");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }

    private async void ListView_AccountSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var list = sender as ListView;
            if (list != null)
            {
                AccountModel? selectedAccount = list.SelectedItem as AccountModel;
                if (selectedAccount != null)
                {
                    _AccountViewModel.SelectedAccount = selectedAccount;
                    await Shell.Current.GoToAsync($"//{nameof(AccountPage)}");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }

    private async void ListView_JobSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var list = sender as ListView;
            if (list != null)
            {
                JobModel? selectedJob = list.SelectedItem as JobModel;
                if (selectedJob != null)
                {
                    _JobViewModel.SelectedJob = selectedJob;
                    await Shell.Current.GoToAsync($"//{nameof(JobPage)}");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            return;
        }
    }
}
