using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels
{
    public partial class TransactionsModel : ObservableObject
    {
        public int Id { get; set; }
        [ObservableProperty]
        double amount;
        public UserModel User { get; set; } = new UserModel();
        [Required]
        public TransactionTypeModel? TransactionType { get; set; } // This is a nullable type

    }
}
