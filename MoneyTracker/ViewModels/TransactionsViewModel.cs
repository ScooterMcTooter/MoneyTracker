using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyTrackerMigrations;
using MoneyTrackerMigrations.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels
{
    public partial class TransactionsViewModel : ObservableObject
    {
        private readonly ApplicationDbContext _db;
        private readonly UserViewModel _userViewModel;

        public TransactionsViewModel(ApplicationDbContext db, UserViewModel userViewModel)
        {
            _db = db;
            _userViewModel = userViewModel;
            SelectedAccount = new AccountModel();
            TransactionType = new TransactionTypeModel();
            User = _db.userModels.Where(u => u.Id == _userViewModel.Id).First() ?? new UserModel();

            Accounts = new ObservableCollection<AccountModel>(_db.accountModels.ToList());
            TransactionTypes = new ObservableCollection<TransactionTypeModel>(_db.transactionTypeModels.ToList());
            Transactions = new ObservableCollection<TransactionModel>(_db.transactionModels.Any() ? _db.transactionModels.Where(u => u.UserId == User.Id).ToList(): new List<TransactionModel>());
        }

        #region Observable Properties
        [ObservableProperty]
        string name = string.Empty;
        [ObservableProperty]
        double amount;
        [ObservableProperty]    
        string? description;
        [ObservableProperty]
        TransactionTypeModel transactionType;
        [ObservableProperty]
        ObservableCollection<TransactionTypeModel> transactionTypes;
        [ObservableProperty]
        ObservableCollection<AccountModel> accounts;
        [ObservableProperty]
        AccountModel selectedAccount;
        [ObservableProperty]
        ObservableCollection<TransactionModel> transactions;
        [ObservableProperty]
        TransactionModel selectedTransaction;
        [ObservableProperty]
        DateTime? date = null;
        #endregion

        public UserModel User { get; set; }
        public string TransactionList
        {
            get => $"{Name}\t{Date?.ToString("MM/dd/yyyy")}";
        }

        #region Commands
        [RelayCommand]
        async Task SaveTransaction()
        {
            // Save transaction to database
            TransactionModel transaction = new TransactionModel
            {
                Name = Name,
                Amount = Amount,
                Notes = Description,
                TransactionType = TransactionType,
                Account = SelectedAccount,
                Date = Date.HasValue ? Date.Value : DateTime.Now,
                User = User
            };

            await _db.transactionModels.AddAsync(transaction);
            await _db.SaveChangesAsync();
        }

        [RelayCommand]
        public void ClearTransaction()
        {
            Name = string.Empty;
            Amount = 0;
            Description = string.Empty;
            TransactionType = new TransactionTypeModel();
        }
        #endregion
    }
}
