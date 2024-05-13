using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class BucketModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string BucketName { get; set; } = "Main Bucket";
    public decimal BucketAmount { get; set; }
    public int AccountId { get; set; }
    public virtual AccountModel? Account { get; set; }
    public virtual ICollection<UserModel>? Users { get; set; }
    public virtual ICollection<TransactionModel>? Transactions { get; set; } = null;
}
