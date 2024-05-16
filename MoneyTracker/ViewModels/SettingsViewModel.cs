using CommunityToolkit.Mvvm.ComponentModel;

namespace MoneyTracker.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Currency { get; set; } = "USD";
    public string? Language { get; set; } = "ENG";
    public bool IsDarkMode { get; set; }
    public bool IsNotificationsEnabled { get; set; }
    public bool IsBiometricEnabled { get; set; }
    public bool IsLocationEnabled { get; set; }
}
