using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyTrackerMigrations.Models;

namespace MoneyTracker.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region ObservableProperties
    [ObservableProperty]
    UserModel? user;
    [ObservableProperty]
    string? currency = "USD";
    [ObservableProperty]
    string? language = "ENG";
    [ObservableProperty]
    bool isDarkMode = true;
    [ObservableProperty]
    bool isNotificationsEnabled = false;
    [ObservableProperty]
    bool isBiometricEnabled = false;
    [ObservableProperty]
    bool isLocationEnabled = false;
    [ObservableProperty]
    bool isDisplayed = false;
    #endregion
    #region RelayCommands
    //[RelayCommand]
    //async Task Save()
    //{
    //    // Save settings

    //}

    //[RelayCommand]
    //async Task Cancel()
    //{
    //    // Cancel changes
    //}

    //[RelayCommand]
    //async Task Reset()
    //{
    //    // Reset settings
    //}

    //[RelayCommand]
    //async Task Logout()
    //{
    //    // Logout user
    //}

    [RelayCommand]
    void SetVisibility()
    {
        IsDisplayed = !IsDisplayed;
    }
    #endregion
}
