using MoneyTracker.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MoneyTracker.Models
{
    public class AccountModel
    {
        [ForeignKey("UserViewModel")]
        [Key]
        public int Id { get; set; } = 0;
        public int UserId { get; set; }
        public virtual UserModel? User { get; set; }
        [Required]
        public string Name { get; set; } = "Savings Default";
        [Required]
        public decimal Balance { get; set; } = 0;
        public string Provider { get; set; } = "Santander";
        public string Type { get; set; } = "Savings";
        [NotMapped]
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        { 
            get => _AccountNumber; 
            set => _AccountNumber = new Helper().HashAccountNumber(value); 
        }
        public string? RoutingNumber { get; set; } = null;
        public virtual ICollection<BucketModel> Buckets { get; set; } = new List<BucketModel>();
        public virtual ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
        public virtual ICollection<AutoPayModel> AutoPays { get; set; } = new List<AutoPayModel>();
    }
}
