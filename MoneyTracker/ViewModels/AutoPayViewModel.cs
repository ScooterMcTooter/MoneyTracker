using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.ViewModels
{
    public partial class AutoPayViewModel : ObservableObject
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserViewModel")]
        [Required]
        public int UserId { get; set; }
        public virtual UserViewModel? User { get; set; }

        [ForeignKey("LoansViewModel")]
        public int LoanId { get; set; }
        public virtual List<LoanViewModel>? Loans { get; set;}

        [ObservableProperty]
        bool autoPayActive;

        [ObservableProperty]
        int autopayDay;

        public DateTime? LastAutopayDate => new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, AutopayDay);

        public DateTime? NextAutoPayDate => new DateTime(DateTime.Today.Year, DateTime.Today.Month, AutopayDay);

        [ForeignKey("TransactionTypeViewModel")]
        public int TransactionTypeId { get; set; }
        public virtual List<TransactionTypeViewModel>? TransactionType { get; set; } 
    }
}