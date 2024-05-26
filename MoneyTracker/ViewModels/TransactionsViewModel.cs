using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels
{
    public partial class TransactionsViewModel : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        [ObservableProperty]
        double amount;
        public UserViewModel? User { get; set; }
        [Required]
        public TransactionTypeViewModel? TransactionType { get; set; } // Connected to Constants.TransactionTypes
        [Required]
        public AccountViewModel account { get; set; } = new AccountViewModel();
        public string? Location { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}
