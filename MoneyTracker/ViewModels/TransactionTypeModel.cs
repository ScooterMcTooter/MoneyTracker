using System.ComponentModel.DataAnnotations;

public class TransactionTypeModel
{
    [Key]
    public int Id { get; set; }
    public string? Type { get; set; }
}
