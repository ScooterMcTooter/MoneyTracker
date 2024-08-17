using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    readonly IServiceProvider _serviceProvider;
    readonly Helper helper;
    public HomeViewModel(ApplicationDbContext db, IServiceProvider serviceProvider)
    {
        _db = db;
        _serviceProvider = serviceProvider;
        helper = _serviceProvider.GetRequiredService<Helper>();

        #region Set Model Properties
        User = Constants.CurrentUser;
        Loans = new ObservableCollection<LoanModel>(Constants.ConstLoans);
        Settings = Constants.ConstSettings;
        Accounts = new ObservableCollection<AccountModel>(Constants.ConstAccounts);
        Autos = new ObservableCollection<AutoPayModel>(Constants.ConstAutoPays);
        Jobs = new ObservableCollection<JobModel>(Constants.ConstJobs);
        Transactions = new ObservableCollection<TransactionModel>(Constants.ConstTransactions);
        #endregion
        #region Model Headers
        AccountHeader = Accounts.Any() ? $"Total Accounts: {AccountCount}" : "You have no accounts!";
        LoanHeader = Loans.Any() ? $"Total Loans: {LoanCount}" : "You have no loans! Go Celebrate!";
        AutoHeader = Autos.Any() ? $"Total AutoPays: {Autos.Count}" : "You have no active AutoPays!";
        TransactionHeader = Transactions.Any() ? $"Total Transactions: {TransactionCount}" : "You have no transactions!";
        JobHeader = Jobs.Any() ? $"Total Jobs: {JobCount}" : "You have no jobs.";
        #endregion
        #region 'Has' Properties
        HasAccounts = Accounts.Any();
        HasAutos = Autos.Any();
        HasLoans = Loans.Any();
        HasTransactions = Transactions.Any();
        HasJobs = Jobs.Any();
        #endregion

        CurrentIncome = helper.PayvsBills(0); //replace with Bills when that gets created
        CurrentCredit = GetCredit();
        CurrentLoans = GetLoans();
        TotalBalance = ((CurrentSavings + CurrentChecking + CurrentCredit) - CurrentLoans);
    }

    #region Properties
    #region Count Properties
    public int LoanCount => Constants.ConstLoans.Count();
    public int AccountCount => Constants.ConstAccounts.Count();
    public int AutoCount => Constants.ConstAutoPays.Count();
    public int TransactionCount => Constants.ConstTransactions.Count();
    public int JobCount => Constants.ConstJobs.Count();
    #endregion
    public double Balance => CurrentIncome - CurrentExpenses;
    public double CurrentDebt => Loans?.Where(x => x.UserId == User.Id).Sum(x => x.Amount) ?? 0;
    public double CurrentNetWorth => TotalBalance - GetLoans();
    #region Color Properties
    public string BalanceColor => helper.GetBalanceColor(Balance);
    public string TotalBalanceColor => helper.GetBalanceColor(TotalBalance);
    public string CurrentNetWorthColor => helper.GetBalanceColor(CurrentNetWorth);
    public string CurrentIncomeColor => helper.GetBalanceColor(CurrentIncome);
    public string CurrentExpensesColor => helper.GetBalanceColor(CurrentExpenses);
    public string CurrentSavingsColor => helper.GetBalanceColor(CurrentSavings);
    public string CurrentCheckingColor => helper.GetBalanceColor(CurrentChecking);
    public string CurrentDebtColor => helper.GetBalanceColor(CurrentDebt, true);
    public string CurrentCreditColor => helper.GetBalanceColor(CurrentCredit, true);
    #endregion
    public string BalanceString => $"${CurrentIncome} - ${CurrentExpenses}:";
    public string Width => (DeviceDisplay.MainDisplayInfo.Width * .3).ToString();
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
    UserModel user;
    [ObservableProperty]
    SettingsModel settings;

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
    [ObservableProperty]
    double currentCredit;
    [ObservableProperty]
    double currentLoans;
    [ObservableProperty]
    ObservableCollection<TransactionModel>? transactions;
    [ObservableProperty]
    TransactionModel? selectedTransaction;
    [ObservableProperty]
    TransactionModel? addTransactionM;
    [ObservableProperty]
    ObservableCollection<AccountModel>? accounts;
    [ObservableProperty]
    AccountModel? selectedAccount;
    [ObservableProperty]
    AccountModel? addAccountM;
    [ObservableProperty]
    ObservableCollection<LoanModel>? loans;
    [ObservableProperty]
    LoanModel? selectedLoan;
    [ObservableProperty]
    LoanModel? addLoanM;
    [ObservableProperty]
    ObservableCollection<AutoPayModel>? autos;
    [ObservableProperty]
    AutoPayModel? selectedAuto;
    [ObservableProperty]
    AutoPayModel? addAutoM;
    [ObservableProperty]
    ObservableCollection<JobModel> jobs;
    [ObservableProperty]
    JobModel? selectedJob;
    [ObservableProperty]
    JobModel? addJobM;
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
    [ObservableProperty]
    bool hasTransactions;
    [ObservableProperty]
    string transactionHeader;
    [ObservableProperty]
    bool hasJobs;
    [ObservableProperty]
    string jobHeader;
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
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
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
            Name = AddTransactionM.Name,
            Amount = AddTransactionM.Amount,
            Date = AddTransactionM.Date,
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
        AccountModel account = new AccountModel { Name = AddAccountM.Name, Balance = AddAccountM.Balance };
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
        LoanModel loan = new LoanModel { Name = AddLoanM.Name ?? "", Amount = AddLoanM.Amount };
        if (string.IsNullOrEmpty(loan.Name))
            return;

        if (await IsNew(loan))
        {

        }
        
        await _db.loanModels.AddAsync(loan);
        await _db.SaveChangesAsync();
        return;
    }

    [RelayCommand]
    async Task EditLoan(LoanModel loan)
    {
        if (loan == null)
            return;

        var pars = new Dictionary<string, LoanModel>
        {
            { "loan", loan }
        };

        await Shell.Current.GoToAsync($"{nameof(LoanPage)}");
        return;
    }
    #endregion

    #region Methods
    private async Task<bool> IsNew(dynamic obj)
    {
        return await Task.Run(() => false );
    }

    private double GetCredit()
    {
        if(User.Id == 0)
            return 0;

        var credit = Math.Round(_db.accountModels.Where(x => x.UserId == User.Id && x.Type.Equals("Credit")).ToList().Sum(x => x.Balance), 2);        
        return (double)credit;
    }

    private double GetLoans()
    {
        if(User.Id == 0)
            return 0;

        var loan = Math.Round(Loans.Where(x => x.UserId == User.Id).ToList().Sum(x => x.RemainingBalance), 2);
        return (double)loan;
    }
    #endregion
}