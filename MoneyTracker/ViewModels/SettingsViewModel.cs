using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using MoneyTrackerMigrations.Models;
using System.Windows.Input;

namespace MoneyTracker.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    //public ICommand SetAppThemeCommand { get; }
    public SettingsViewModel()
    {
        Theme = Application.Current.RequestedTheme == AppTheme.Dark ? "Dark Mode" : "Light Mode";
        IsDarkMode = App.Current.UserAppTheme == AppTheme.Dark ? true : false;
        // Load settings
    }
    
    #region ObservableProperties
    [ObservableProperty]
    UserModel? user;
    [ObservableProperty]
    string? currency = "USD";
    [ObservableProperty]
    string? language = "ENG";
    [ObservableProperty]
    bool isDarkMode = App.Current.UserAppTheme == AppTheme.Dark ? true : false;
    [ObservableProperty]
    string theme = App.Current.UserAppTheme == AppTheme.Unspecified || App.Current.UserAppTheme == AppTheme.Dark ? "Dark Mode" : "Light Mode";
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
    void SetAppTheme()
    {
        try
        {
            IsDarkMode = !IsDarkMode;
            Theme = IsDarkMode ? "Dark Mode" : "Light Mode";
            App.Current.UserAppTheme = IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        }
        catch (Exception ex)
        {
            throw;
        }
        return;
    }
    #endregion
}
