using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class TransactionTypeModel
{
    [Key]
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
}
