using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class TransactionModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now.Date;
    public TransactionType TransactionType { get; set; }
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public int AutoPayId { get; set; }
    public virtual AutoPayModel? AutoPay { get; set; }
    public int BucketId { get; set; }
    public virtual BucketModel? Bucket { get; set; }
    public int AccountId { get; set; }
    public virtual AccountModel? Account { get; set; }
    public int LoanId { get; set; }
    public virtual LoanModel? Loan { get; set; }
}

public enum TransactionType
{
    Cash,
    Debit,
    Credit,
    CreditCard,
    ApplePay,
    Venmo,
    PayPal,
    ACHRecurring,
    ACHOnce,
    Check,
    CashApp,
    InternalTransfer,
    ExternalTransfer,
    Deposit,
    ATMWithdrawal,
    ATMDeposit,
    MobileDeposit,
    Other
}
