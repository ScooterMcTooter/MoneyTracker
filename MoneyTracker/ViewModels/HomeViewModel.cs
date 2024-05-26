using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyTracker.Pages;

namespace MoneyTracker.ViewModels;

public partial class HomeViewModel : ObservableValidator
{
    public int Id { get; set; }
    public virtual LoansViewModel? Loans { get; set; }
    public decimal CurrentIncome { get; set; }
    public decimal CurrentExpenses { get; set; }
    public decimal CurrentSavings { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal CurrentDebt { get; set; }
    public decimal CurrentNetWorth { get; set; }
    private UserViewModel _user;
    public virtual UserViewModel User { get; set; }

    /// <summary>
    /// Logs out the user.
    /// </summary>
    [RelayCommand]
    async Task Logout()
    {
        Constants.IsAuthenticated = false;
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
