using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace MoneyTracker.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    private readonly ApplicationDbContext _db;
    private readonly IDialogService _dialogService;
    private readonly IServiceProvider _serviceProvider;

    public AccountViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _db = _serviceProvider.GetService<ApplicationDbContext>() ?? throw new NotImplementedException("There is a failure when trying to access the ApplicationDbContext service.");
        _dialogService = _serviceProvider.GetService<IDialogService>() ?? throw new NotImplementedException("There is a failure when trying to access the IDialogService service.");

        Accounts = new ObservableCollection<AccountModel>(Constants.ConstAccounts ?? []);
        Jobs = new ObservableCollection<JobModel>(Constants.ConstJobs.Where(j => j.UserId == Constants.CurrentUser.Id) ?? []);
        Buckets = new ObservableCollection<BucketModel>(Constants.ConstBuckets.Where(b => Constants.ConstBuckets.Any(a => a.AccountId == b.AccountId)) ?? []);
        Transactions = new ObservableCollection<TransactionModel>(Constants.ConstTransactions.Where(t => t.UserId == Constants.CurrentUser.Id) ?? []);
        AutoPays = new ObservableCollection<AutoPayModel>(Constants.ConstAutoPays.Where(a => a.UserId == Constants.CurrentUser.Id) ?? []);

        LastFour = string.IsNullOrEmpty(AccountNumber) ? string.Empty : AccountNumber.Substring(AccountNumber.Length - 4);
        SelectedAccount = Accounts.FirstOrDefault() ?? new AccountModel();
        CreateMessage();

        AddAccountText = AddAccountVisible ? "Cancel" : "Add Account";
    }

    #region Properties
    public double Width => Constants.maxWidth;
    #endregion

    #region Observable Properties
    [ObservableProperty]
    AccountModel selectedAccount;
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
    [ObservableProperty]
    string lastFour;
    [ObservableProperty]
    string accountNumber = string.Empty;
    [ObservableProperty]
    float balance;
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

        var edit = Constants.ConstAccounts.Where(a => a.UserId == Constants.CurrentUser.Id && a.AccountNumber.Equals(AccountNumber));

        if (edit != null)
        {
            _db.accountModels.Update(account);
        }
        else
        {
            _db.accountModels.Add(account);
        }

        _db.SaveChanges();

        int eId = Constants.CurrentUser.Id;
        Constants.ConstAccounts = _db.accountModels.Where(a => a.UserId == eId).ToList();
        Accounts = new ObservableCollection<AccountModel>(Constants.ConstAccounts);
        CreateMessage();

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
        AccountNumber = string.Empty;
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
    async Task DeleteAccount(AccountModel account)
    {
        bool confirmed = await _dialogService.ShowConfirmationDialogAsync("Account Deletion", "Are you sure you want to delete this account?", "Yes", "Cancel");

        if (!confirmed)
            return;

        CreateMessage();
        _db.accountModels.Remove(account);
        _db.SaveChanges();

        Accounts.Remove(account);
        Constants.ConstAccounts = Accounts.ToList();
        return;
    }

    /// <summary>
    /// Edits the specified account.
    /// </summary>
    /// <param name="account">The account to edit.</param>
    [RelayCommand]
    void EditAccount(AccountModel account)
    {
        AddAccount();
        Accounts.Remove(account);
        Constants.ConstAccounts.Remove(account);

        Name = account.Name;
        AccountNumber = account.AccountNumber;
        Balance = (float)account.Balance;
        Provider = account.Provider;
        Type = account.Type;
        RoutingNumber = account.RoutingNumber ?? string.Empty;

        _db.accountModels.Update(account);
        _db.SaveChanges();
        Accounts.Add(account);
        Constants.ConstAccounts.Add(account);
        return;
    }
    #endregion

    #region Methods
    private void CreateMessage()
    {
        if (Accounts.Count == 0)
            Message = $"Welcome to the Account Page! Once you have some accounts they will be displayed here!";
        else if (Accounts.Count > 1)
            Message = Accounts.Sum(a => a.Balance) > 0 ? $"Your account has a balance of ${Accounts.Sum(a => a.Balance)}!" : $"Your accounts balances equal out to {Accounts.Sum(a => a.Balance)}";
        return;
    }
    #endregion
}
