﻿using MoneyTracker.Pages;

namespace MoneyTracker; 

public partial class AppShell : Shell
{
    public AppShell(ViewModels.SettingsViewModel settingsViewModel)
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(PasswordResetPage), typeof(PasswordResetPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
        Routing.RegisterRoute(nameof(TransactionsPage), typeof(TransactionsPage));
    }
}
