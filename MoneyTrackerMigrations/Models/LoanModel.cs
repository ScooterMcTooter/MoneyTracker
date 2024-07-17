using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class LoanModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public double InterestRate { get; set; }
    public double TotalInterest { get; set; }
    public double MonthlyPayment { get; set; }
    public double RemainingBalance { get; set; }
    public double RemainingInterest { get; set; }
    public string LoanType { get; set; } = string.Empty;
    public string Servicer { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool MonthlyPaid { get; set; }
    public string LoanStatus { get; set; } = string.Empty;
    public string? StudentLoanType { get; set; } = null;
    public string? FedLoanType { get; set; } = null;
    public string? InterestType { get; set; } = null;
    public string? SchoolName { get; set; } = null;
    public string CurrentOwner { get; set; } = string.Empty;
    public string Guarantor { get; set; } = string.Empty;
    public DateTime DisbursementDate { get; set; }
    public string? RepaymentPlan { get; set; } = null;
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public virtual ICollection<TransactionModel>? Transactions { get; set; }
    public int? AutoPayId { get; set; }
    public virtual AutoPayModel? AutoPays { get; set; }

}