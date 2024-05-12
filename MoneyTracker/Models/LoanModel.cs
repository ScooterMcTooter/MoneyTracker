using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Models;

public class LoanModel
{
    [Key]
    public int Id { get; set; }
    public string LoanName { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal InterestRate { get; set; }
    public decimal MonthlyPayment { get; set; }
    public decimal RemainingBalance { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool PaidOff { get; set; }
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public virtual ICollection<TransactionModel>? Transactions { get; set; }
    public virtual ICollection<AutoPayModel>? AutoPay { get; set; }
}
