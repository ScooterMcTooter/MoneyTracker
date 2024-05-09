using System.ComponentModel.DataAnnotations;

public class TransactionTypeViewModel
{
    [Key]
    public int Id { get; set; }
    public string? Type { get; set; }
}
