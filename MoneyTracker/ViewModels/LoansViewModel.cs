using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels
{
    public partial class LoansViewModel : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        public UserViewModel? User { get; set; }
        public AccountViewModel? LinkedAccount { get; set; } = new AccountViewModel();
        [ObservableProperty]
        double loanAmount;
        [ObservableProperty]
        double totalInterest;
        [ObservableProperty]
        double interestRate;
        [ObservableProperty]
        double monthlyPayment;
        public AutoPayViewModel? autoPay { get; set; } 
        [ObservableProperty]
        double totalPayment;
        [ObservableProperty]
        int? loanTerm = null;
        [ObservableProperty]
        string? loanType;
    }
}
