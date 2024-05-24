using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Pages;
using MoneyTrackerMigrations;
using MoneyTrackerMigrations.Models;
using System.Collections.ObjectModel;

namespace MoneyTracker.ViewModels;

/// <summary>
/// Message classes used to scroll to a specific location.
/// </summary>
public class ScrollToMessage {}
public class ScrollToTop {}

/// <summary>
/// ViewModel for the Home page.
/// </summary>
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
        Loan = new LoansViewModel();
        Settings = new SettingsViewModel();
        Transaction = new TransactionsViewModel(_db,User);
        Transactions = new ObservableCollection<TransactionModel>();
        Account = new AccountViewModel(); // Initialize Account property
        Accounts = new ObservableCollection<AccountModel>(_db.accountModels.Where(x => x.UserId == Id).ToList()); // Initialize Accounts property
        HasAccounts = Accounts.Count > 0;
        AccountHeader = Accounts.Count > 0 ? $"Total Accounts: {AccountCount}" : "You have no accounts!";
        LoanHeader = LoanCount > 0 ? $"Total Loans: {LoanCount}" : "You have no loans! Go Celebrate!";
        AutoHeader = AutoCount > 0 ? $"Total AutoPays: {Autos.Count}" : "You have no active AutoPays!";
        HasAutos = AutoCount > 0;
    }

    #region Properties
    public int Id { get; set; }
    public virtual LoansViewModel? Loan { get; set; }
    public int LoanCount => _db.loanModels.Where(x => x.UserId == Id).Count();
    public int AccountCount => _db.accountModels.Where(x => x.UserId == Id).Count();
    public int AutoCount => _db.autoPayModels.Where(x => x.UserId == Id).Count();
    private UserViewModel _user;
    public virtual UserViewModel User { get; set; }
    public virtual SettingsViewModel Settings { get; set; }
    public virtual TransactionsViewModel Transaction { get; set; }
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
    public virtual ObservableCollection<TransactionModel> Transactions { get; set; } = new ObservableCollection<TransactionModel>(); // Initialize Transactions property
    public virtual ObservableCollection<AccountModel> Accounts { get; set; } = new ObservableCollection<AccountModel>(); // Initialize Accounts property
    [ObservableProperty]
    AccountModel selectedAccount;
    public virtual ObservableCollection<LoanModel> Loans { get; set; } = new ObservableCollection<LoanModel>(); // Initialize Loans property
    [ObservableProperty]
    LoanModel selectedLoan;
    public virtual ObservableCollection<AutoPayModel> Autos { get; set; } = new ObservableCollection<AutoPayModel>(); // Initialize Goals property
    [ObservableProperty]
    AutoPayModel selectedAuto;
    [ObservableProperty]
    bool chartVis;
    [ObservableProperty]
    bool quickAddVis;
    [ObservableProperty]
    string buttonText = "Quick Add";
    [ObservableProperty]
    bool hasAccounts;
    [ObservableProperty]
    string accountHeader;
    [ObservableProperty]
    bool hasLoans;
    [ObservableProperty]
    string loanHeader;
    [ObservableProperty]
    bool hasAutos;
    [ObservableProperty]
    string autoHeader;
    #endregion
    #region Relay Commands
    /// <summary>
    /// Logs out the user and navigates to the Login page.
    /// </summary>
    [RelayCommand]
    async Task Logout()
    {
        Constants.IsAuthenticated = false;
        // Clear user-specific data
        // This depends on how you're storing data
        // For example, if you're using Xamarin.Essentials SecureStorage:
        SecureStorage.RemoveAll();

        // Reset the navigation stack
        // This depends on how you're handling navigation
        // For example, if you're using Shell navigation:
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    /// <summary>
    /// Toggles the visibility of the chart.
    /// </summary>
    [RelayCommand]
    void Chart()
    {
        ChartVis = !ChartVis;
        return;
    }

    /// <summary>
    /// Toggles the visibility of the Quick Add form and sends a message to scroll to it when it becomes visible.
    /// </summary>
    [RelayCommand]
    void QuickAdd()
    {
        QuickAddVis = !QuickAddVis;
        ButtonText = QuickAddVis ? "Close" : "Quick Add";
        if (QuickAddVis)
            WeakReferenceMessenger.Default.Send(new ScrollToMessage());
        else
            WeakReferenceMessenger.Default.Send(new ScrollToTop());
        return;
    }

    /// <summary>
    /// Adds a new transaction to the database.
    /// </summary>
    [RelayCommand]
    async Task AddTransaction()
    {
        TransactionModel transaction = new TransactionModel
        {
            Name = Transaction.Name,
            Amount = Transaction.Amount,
            Date = Transaction.Date.GetValueOrDefault(),
        };

        if (Transactions.Count == 0)
            return;

        await _db.transactionModels.AddRangeAsync(Transactions);
        await _db.SaveChangesAsync();
        return;
    }

    /// <summary>
    /// Adds a new account to the database.
    /// </summary>
    [RelayCommand]
    async Task AddAccount()
    {
        AccountModel account = new AccountModel { Name = Account.AccountName, Balance = Account.AccountNumber };
        if (string.IsNullOrEmpty(account.Name))
            return;

        await _db.accountModels.AddAsync(account);
        await _db.SaveChangesAsync();
        return;
    }

    /// <summary>
    /// Adds a new loan to the database.
    /// </summary>
    [RelayCommand]
    async Task AddLoan()
    {
        LoanModel loan = new LoanModel { LoanName = Loan.LoanName ?? "", LoanAmount = Loan.LoanAmount };
        if (string.IsNullOrEmpty(loan.LoanName))
            return;

        await _db.loanModels.AddAsync(loan);
        await _db.SaveChangesAsync();
        return;
    }
    #endregion
}
