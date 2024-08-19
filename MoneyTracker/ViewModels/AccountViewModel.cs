using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTrackerMigrations.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
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
        AccountTypeValues = new ObservableCollection<AccountType>(Enum.GetValues(typeof(AccountType)).Cast<AccountType>());
        selectedAccountType = Enum.TryParse(SelectedAccount?.Type, out AccountType type) ? type : AccountType.Checking;

        CreateMessage();

        IsEdit = true;
        AddAccountText = AddAccountVisible ? "Cancel" : "Add Account";
    }

    #region Properties
    public double Width => Constants.maxWidth;
    #endregion

    #region Observable Properties
    [ObservableProperty]
    ObservableCollection<AccountType> accountTypeValues;
    [ObservableProperty]
    AccountType? accountTypeEnum;
    [ObservableProperty]
    AccountType selectedAccountType = AccountType.Checking;
    [ObservableProperty]
    AccountModel? selectedAccount;
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
    bool isEdit;
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
    void SaveAccount(AccountModel _account)
    {
        AccountModel a = new AccountModel();
        _account.Type = SelectedAccountType.ToString();
        var existingEntity = _db.accountModels.FirstOrDefault(e => e.Id == _account.Id);
        
        try
        {
            if (existingEntity != null)
            {
                a = new AccountModel
                {
                    Id = existingEntity.Id,
                    UserId = Constants.CurrentUser.Id,
                    Name = Name,
                    Balance = (decimal)Balance,
                    Provider = Provider,
                    AccountNumber = AccountNumber,
                    RoutingNumber = RoutingNumber,
                    Type = SelectedAccountType.ToString()
                };

                _db.Entry(existingEntity).State = EntityState.Detached;
            }

            if ((_serviceProvider.GetService<Helper>() ?? new Helper()).IsChanged(a, existingEntity))
            {
                _db.accountModels.Update(a);
            }
            else
            {
                _db.accountModels.Add(a);
            }

            IsEdit = true;
            _db.SaveChanges();

            int eId = Constants.CurrentUser.Id;
            Constants.ConstAccounts = _db.accountModels.Where(a => a.UserId == eId).ToList();
            Accounts = new ObservableCollection<AccountModel>(Constants.ConstAccounts);
            CreateMessage();

            AddAccountVisible = false;
            AddAccountText = "Add Account";
        }
        catch (Exception ex)
        {
            // Handle exception
            throw;
        }
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
        //Ensure that the _db is not already being used


        if (account == null)
        {
            return;
        }
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
        try
        {
            SelectedAccount = account;
            SelectedAccount.Type = SelectedAccountType.ToString();

            var existingEntity = _db.accountModels.FirstOrDefault(e => e.Id == account.Id);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).State = EntityState.Detached;
            }

            AddAccount();

            Name = account.Name;
            AccountNumber = account.AccountNumber;
            Balance = (float)account.Balance;
            Provider = account.Provider;
            RoutingNumber = account.RoutingNumber ?? string.Empty;
            LastFour = account.AccountNumber.Substring(account.AccountNumber.Length > 0 ? account.AccountNumber.Length - 4 : 0);
            IsEdit = false;
        }
        catch (Exception ex)
        {
            // Handle exception
            Shell.Current.DisplayAlert("Error", $"An error occurred while trying to edit the account. \r\n {ex}", "OK");
            return;
        }

        return;
    }

    #endregion

    #region Methods
    private void CreateMessage()
    {
        if (Accounts.Count == 0)
            Message = $"Welcome to the Account Page! Once you have some accounts they will be displayed here!";
        else if (Accounts.Count > 1)
            Message = Accounts.Sum(a => a.Balance) > 0 ? $"Your account has a balance of\r\n${Accounts.Sum(a => a.Balance)}!" : $"Your accounts balances equal out to {Accounts.Sum(a => a.Balance)}";
        return;
    }

    partial void OnAccountNumberChanged(string value)
    {
        LastFour = value.Length >= 4 ? value[^4..] : value;
    }
    #endregion

    #region Enums
    public enum AccountType
    {
        Checking,
        Savings,
        Credit,
        Loan
    }
    #endregion
}
