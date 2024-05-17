using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using MoneyTrackerMigrations.Models;

namespace MoneyTracker.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel()
    {
        ThemeButton = App.Current.UserAppTheme == AppTheme.Dark || App.Current.UserAppTheme == AppTheme.Unspecified ? "Dark Mode" : "Light Mode";
        // Load settings
    }
    #region Properties
    #endregion
    #region ObservableProperties
    [ObservableProperty]
    UserModel? user;
    [ObservableProperty]
    string? currency = "USD";
    [ObservableProperty]
    string? language = "ENG";
    [ObservableProperty] string? themeButton;
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

    [RelayCommand]
    async Task SetDarkMode()
    {
        try
        {
            IsDarkMode = !IsDarkMode;
            ThemeButton = IsDarkMode ? "Light Mode" : "Dark Mode";
            AppTheme theme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
            if (App.Current != null)
                App.Current.UserAppTheme = theme;
            else
                App.Current.UserAppTheme = AppTheme.Dark;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            throw;
        }
        return;
    }
    #endregion
}
