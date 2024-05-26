using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class AutoPayModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string AutoPayName { get; set; } = "Default";
    [Required]
    public decimal AutoPayAmount { get; set; }
    public DateTime AutoPayDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 0);
    public bool AutoPayActive { get; set; }
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    [Required]
    public int AccountId { get; set; }
    public virtual AccountModel? Accounts { get; set; }
    public int LoanId { get; set; }
    public virtual LoanModel? Loan { get; set; } = null;
    public virtual ICollection<TransactionModel>? Transactions { get; set; } = null;
    public int TransactionTypeId { get; set; }
    public virtual TransactionTypeModel? TransactionType { get; set; }
}
