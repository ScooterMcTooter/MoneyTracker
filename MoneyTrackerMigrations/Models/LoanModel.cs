using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class LoanModel
{
    [Key]
    public int Id { get; set; }
    public string LoanName { get; set; } = string.Empty;
    public double LoanAmount { get; set; }
    public double InterestRate { get; set; }
    public double MonthlyPayment { get; set; }
    public double RemainingBalance { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool PaidOff { get; set; }
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public virtual ICollection<TransactionModel>? Transactions { get; set; }
    public virtual ICollection<AutoPayModel>? AutoPay { get; set; }
}
