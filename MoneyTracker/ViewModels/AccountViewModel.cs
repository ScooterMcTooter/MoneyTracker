using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace MoneyTracker.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    private readonly ApplicationDbContext _db;
    private readonly IServiceProvider _serviceProvider;

    public AccountViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _db = _serviceProvider.GetService<ApplicationDbContext>() ?? throw new NotImplementedException("There is a failure when trying to access the ApplicationDbContext service.");

        Accounts = new ObservableCollection<AccountModel>(Constants.ConstAccounts ?? []);
        Jobs = new ObservableCollection<JobModel>(Constants.ConstJobs.Where(j => j.UserId == Constants.CurrentUser.Id) ?? []);
        Buckets = new ObservableCollection<BucketModel>(Constants.ConstBuckets.Where(b => Constants.ConstBuckets.Any(a => a.AccountId == b.AccountId)) ?? []);
        Transactions = new ObservableCollection<TransactionModel>(Constants.ConstTransactions.Where(t => t.UserId == Constants.CurrentUser.Id) ?? []);
        AutoPays = new ObservableCollection<AutoPayModel>(Constants.ConstAutoPays.Where(a => a.UserId == Constants.CurrentUser.Id) ?? []);

        if (Accounts.Count == 0)
            Message = $"Welcome to the Account Page! Once you have some accounts they will be displayed here!";
        else if (Accounts.Count > 1)
            Message = $"You have {Accounts.Count} accounts!";
        else
            Message = Accounts.Sum(a => a.Balance) > 0 ? $"Your account has a balance of ${Accounts.Sum(a => a.Balance)}!" : $"Your accounts balances equal out to {Accounts.Sum(a => a.Balance)}";

        AddAccountText = AddAccountVisible ? "Cancel" : "Add Account";
    }

    #region Properties
    public double Width => Constants.maxWidth;

    private string _accountNumber;
    public string AccountNumber
    {
        get => _accountNumber;
        set
        {
            _accountNumber = value;
            OnPropertyChanged(nameof(AccountNumber));
            OnPropertyChanged(nameof(FormattedAccountNumber));
        }
    }

    public string FormattedAccountNumber
    {
        get
        {
            // Insert your formatting logic here, similar to the converter example
            return Regex.Replace(_accountNumber, ".{4}", "$0-").TrimEnd('-');
        }
    }
    #endregion

    #region Observable Properties
    [ObservableProperty]
    string message = string.Empty;
    [ObservableProperty]
    bool addAccountVisible = false;
    [ObservableProperty]
    string addAccountText = "Add Account";
    [ObservableProperty]
    ObservableCollection<AccountModel> accounts;
    [ObservableProperty]
    string name = string.Empty;
    //[ObservableProperty]
    //int accountNumber;
    [ObservableProperty]
    double balance;
    [ObservableProperty]
    string provider = string.Empty;
    [ObservableProperty]
    string type = string.Empty;
    [ObservableProperty]
    string routingNumber = string.Empty;
    [ObservableProperty]
    ObservableCollection<BucketModel> buckets = [];
    [ObservableProperty]
    ObservableCollection<TransactionModel> transactions = [];
    [ObservableProperty]
    ObservableCollection<AutoPayModel> autoPays = [];
    [ObservableProperty]
    ObservableCollection<JobModel> jobs = [];
    #endregion

    #region Commands    
    /// <summary>
    /// Adds an account.
    /// </summary>
    [RelayCommand]
    void AddAccount()
    {
        AddAccountVisible = !AddAccountVisible;
        AddAccountText = AddAccountVisible ? "Cancel" : "Add Account";
        ClearAccount();
        return;
    }

    /// <summary>
    /// Saves the account.
    /// </summary>
    [RelayCommand]
    void SaveAccount()
    {
        AccountModel account = new AccountModel()
        {
            Name = Name,
            AccountNumber = AccountNumber.ToString(),
            Balance = (decimal)Balance,
            Provider = Provider,
            Type = Type,
            RoutingNumber = RoutingNumber,
            UserId = Constants.CurrentUser.Id
        };

        _db.accountModels.Add(account);
        _db.SaveChanges();

        Accounts.Add(account);
        AddAccountVisible = false;
        AddAccountText = "Add Account";
        return;
    }

    /// <summary>
    /// Clears the account fields.
    /// </summary>
    [RelayCommand]
    void ClearAccount()
    {
        Name = string.Empty;
        AccountNumber = 0;
        Balance = 0;
        Provider = string.Empty;
        Type = string.Empty;
        RoutingNumber = string.Empty;
        return;
    }

    /// <summary>
    /// Deletes the specified account.
    /// </summary>
    /// <param name="account">The account to delete.</param>
    [RelayCommand]
    void DeleteAccount(AccountModel account)
    {
        _db.accountModels.Remove(account);
        _db.SaveChanges();

        Accounts.Remove(account);
        return;
    }

    /// <summary>
    /// Edits the specified account.
    /// </summary>
    /// <param name="account">The account to edit.</param>
    [RelayCommand]
    void EditAccount(AccountModel account)
    {
        Name = account.Name;
        AccountNumber = int.Parse(account.AccountNumber);
        Balance = (double)account.Balance;
        Provider = account.Provider;
        Type = account.Type;
        RoutingNumber = account.RoutingNumber ?? string.Empty;
        return;
    }
    #endregion
}
