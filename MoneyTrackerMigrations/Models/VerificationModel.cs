using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class VerificationModel
{
    [Key]
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? VerificationCode { get; set; }
    public DateTime Expiration { get; set; } = DateTime.Now.AddMinutes(15);
    public bool Used { get; set; } = false;
    public bool Expired { get; set; } = false;
    public bool Verified { get; set; } = false;
    public int UserId { get; set; }
    public UserModel? User { get; set; }
}
