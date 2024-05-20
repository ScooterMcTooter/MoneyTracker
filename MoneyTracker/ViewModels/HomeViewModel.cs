using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Pages;
using MoneyTrackerMigrations.Models;
using System.Collections.ObjectModel;

namespace MoneyTracker.ViewModels;

public partial class HomeViewModel : ObservableValidator
{
    readonly ApplicationDbContext _db;
    readonly Helper helper;
    public HomeViewModel(ApplicationDbContext db)
    {
        _db = db;
        helper = new Helper();
        User = new UserViewModel(_db);
        _user = User;
        Loans = new LoansViewModel();
        Settings = new SettingsViewModel();
        Transactions = new ObservableCollection<TransactionModel>();
        Account = new AccountViewModel(); // Initialize Account property
    }

    #region Properties
    public int Id { get; set; }
    public virtual LoansViewModel? Loans { get; set; }
    public int LoanCount => _db.loanModels.Where(x => x.UserId == Id).Count();
    public int AccountCount => _db.accountModels.Where(x => x.UserId == Id).Count();
    private UserViewModel _user;
    public virtual UserViewModel User { get; set; }
    public virtual SettingsViewModel Settings { get; set; }
    public virtual AccountViewModel Account { get; set; }
    public double Balance => CurrentIncome - CurrentExpenses;
    public double CurrentDebt => _db.loanModels.Where(x => x.UserId == Id).Sum(x => x.LoanAmount);
    public double CurrentNetWorth => (CurrentChecking + CurrentSavings) - CurrentDebt;
    public string BalanceColor => helper.GetBalanceColor(Balance);
    public string TotalBalanceColor => helper.GetBalanceColor(TotalBalance);
    public string CurrentNetWorthColor => helper.GetBalanceColor(CurrentNetWorth);
    public string CurrentIncomeColor => helper.GetBalanceColor(CurrentIncome);
    public string CurrentExpensesColor => helper.GetBalanceColor(CurrentExpenses);
    public string CurrentSavingsColor => helper.GetBalanceColor(CurrentSavings);
    public string CurrentCheckingColor => helper.GetBalanceColor(CurrentChecking);
    public string BalanceString => $"${CurrentIncome} - ${CurrentDebt}:";
    public string Width => (DeviceDisplay.MainDisplayInfo.Width * .2).ToString();
    #endregion

    //set the background color of the Frame to the color of the user's theme
    public string FrameColor => Application.Current.RequestedTheme switch
    {
        AppTheme.Light => "#f0f0f0",
        AppTheme.Dark => "#121212",
        _ => "#f0f0f0"
    };

    public string TextColor => Application.Current.RequestedTheme switch
    {
        AppTheme.Light => "#000000",
        AppTheme.Dark => "#ffffff",
        _ => "#000000"
    };

    #region Observable Properties
    [ObservableProperty]
    double totalBalance;
    [ObservableProperty]
    double currentIncome;
    [ObservableProperty]
    double currentExpenses;
    [ObservableProperty]
    double currentSavings;
    [ObservableProperty] 
    double currentChecking;
    public virtual ICollection<TransactionModel> Transactions { get; set; } = new ObservableCollection<TransactionModel>(); // Initialize Transactions property
    [ObservableProperty]
    bool chartVis = false;
    #endregion

    #region Relay Commands
    /// <summary>
    /// Logs out the user.
    /// </summary>
    [RelayCommand]
    async Task Logout()
    {
        Constants.IsAuthenticated = false;
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    [RelayCommand]
    void Chart()
    {
        ChartVis = !ChartVis;
        return;
    }

    [RelayCommand]
    async Task AddTransaction()
    {
        if (Transactions.Count == 0)
            return;

        await _db.transactionModels.AddRangeAsync(Transactions);
        await _db.SaveChangesAsync();
        return;
    }

    [RelayCommand]
    async Task AddAccount()
    {
        await Task.Yield(); // Add await to make the method asynchronous
        return;
    }

    [RelayCommand]
    async Task AddLoan()
    {
        await Task.Yield(); // Add await to make the method asynchronous
        return;
    }
    #endregion
}
