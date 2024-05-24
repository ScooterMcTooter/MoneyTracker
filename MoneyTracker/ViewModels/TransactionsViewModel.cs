using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            SelectedTransaction = new TransactionModel();
            TransactionType = new TransactionTypeModel();
            var vwId = _userViewModel.Id;
            User = _db.userModels.Where(u => u.Id == vwId).FirstOrDefault() ?? new UserModel();

            Accounts = new ObservableCollection<AccountModel>(_db.accountModels.ToList());
            TransactionTypes = new ObservableCollection<TransactionTypeModel>(_db.transactionTypeModels.ToList());
            Transactions = new ObservableCollection<TransactionModel>(_db.transactionModels.Any() ? _db.transactionModels.Where(u => u.UserId == User.Id).ToList() : new List<TransactionModel>());
        }

        public DateTime MinDate = DateTime.Now.AddMonths(-1);
        public DateTime MaxDate = DateTime.Now.AddMonths(1);

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
        [ObservableProperty]
        string searchName = string.Empty;
        [ObservableProperty]
        DateTime searchDateStart = DateTime.Now.AddMonths(-1);
        [ObservableProperty]
        DateTime searchDateEnd = DateTime.Now.AddMonths(1);
        #endregion

        public UserModel User { get; set; }
        public string TransactionList
        {
            get => $"{Name}:\t{Amount} - {Date?.ToString("MM/dd/yyyy")}";
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
        [RelayCommand]
        public void Search()
        {
            if (SearchName != string.Empty)
            {
                NameSearch();
            }
            else if (SearchDateStart != DateTime.MinValue && SearchDateEnd != DateTime.MaxValue)
            {
                DateSearch();
            }
            else
            {
                Transactions = new ObservableCollection<TransactionModel>(_db.transactionModels.Where(u => u.UserId == User.Id).ToList());
            }
        }
        #endregion

        [RelayCommand]
        public void NameSearch()
        {
            Transactions = new ObservableCollection<TransactionModel>(_db.transactionModels.Where(u => u.Name.Contains(SearchName)).ToList());
        }

        [RelayCommand]
        public void DateSearch()
        {
            if (SearchDateStart.Date == SearchDateEnd.Date)
                Transactions = new ObservableCollection<TransactionModel>(_db.transactionModels.Where(u => u.Date == SearchDateStart).ToList());
            else
                Transactions = new ObservableCollection<TransactionModel>(_db.transactionModels.Where(u => u.Date >= SearchDateStart && u.Date <= SearchDateEnd).ToList());
        }
    }
}
