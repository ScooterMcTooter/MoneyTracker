using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Suffix { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public int PhoneNumber { get; set; }
    public bool MFA { get; set; } = false;
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{5,20}$", ErrorMessage = Constants.UsernameReq)]
    [Required]
    public string? UserName { get; set; }
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordReq)]
    [Required]
    public string? Password { get; set; }
    public DateTime Dob { get; set; }
    public virtual ICollection<LoanModel>? Loans { get; set; } = null;
    public virtual ICollection<AccountModel> Accounts { get; set; } = new List<AccountModel>();
    public virtual ICollection<TransactionModel>? Transactions { get; set; } = null;
    public virtual ICollection<AutoPayModel>? AutoPay { get; set; } = null;
    public virtual ICollection<BucketModel>? Buckets { get; set; } = null;
    public virtual ICollection<JobModel>? Jobs { get; set; } = null;
    public virtual SettingsModel? Settings { get; set; } = null;
}
