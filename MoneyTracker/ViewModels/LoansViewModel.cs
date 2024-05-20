using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.ViewModels
{
    public partial class LoansViewModel : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("LoansViewModel")]
        public int UserId { get; set; }
        public virtual UserViewModel? User { get; set; }
        [ForeignKey("AccountViewModel")]
        public int AccountId { get; set; }
        public virtual AccountViewModel? LinkedAccount { get; set; } = new AccountViewModel();
        [ObservableProperty]
        double loanAmount;
        [ObservableProperty]
        double totalInterest;
        [ObservableProperty]
        double interestRate;
        [ObservableProperty]
        double monthlyPayment; 
        [ForeignKey("AutoPayViewModel")]
        public int AutoPayId { get; set; }
        public virtual AutoPayViewModel? autoPay { get; set; } 
        [ObservableProperty]
        double totalPayment;
        [ObservableProperty]
        int? loanTerm = null;
        [ObservableProperty]
        string? loanType;
        [ObservableProperty]
        string? loanName;
    }
}
