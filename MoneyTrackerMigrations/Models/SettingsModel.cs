using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTrackerMigrations.Models;

public class SettingsModel
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public string? Currency { get; set; } = "USD";
    public string? Language { get; set; } = "ENG";
    public bool IsDarkMode { get; set; } = false;
    public bool IsNotificationsEnabled { get; set; } = false;
    public bool IsBiometricEnabled { get; set; } = false;
    public bool IsLocationEnabled { get; set; } = false;
    [NotMapped]
    public bool IsMFAEnabled { get; set; } = false;
}
