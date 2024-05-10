using MoneyTracker.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TransactionTypeViewModel
{
    [ForeignKey("AutoPayModel")]
    [Key]
    public int Id { get; set; }
    public virtual AutoPayViewModel? AutoPay { get; set; }
    public string? Type { get; set; }
}
