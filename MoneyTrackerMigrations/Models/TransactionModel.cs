using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class TransactionModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public double Amount { get; set; }
    public string? Notes { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.Now.Date;
    public int TransactionTypeId { get; set; }
    public TransactionTypeModel? TransactionType { get; set; }
    public string? OtherDescription { get; set; } = null;
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
