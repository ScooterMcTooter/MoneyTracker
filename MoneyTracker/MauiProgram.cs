﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyTracker.Pages;
using MoneyTracker.ViewModels;

namespace MoneyTracker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<CreateUserPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<PasswordResetPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<TransactionsPage>();
        builder.Services.AddTransient<UserPage>();

        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<AutoPayViewModel>();
        builder.Services.AddTransient<CreateUserViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoansViewModel>();
        builder.Services.AddTransient<SavingsBucketsViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<TransactionTypeViewModel>();
        builder.Services.AddTransient<TransactionsViewModel>();
        builder.Services.AddTransient<UserViewModel>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
#if DEBUG
            options.UseSqlite("Data Source=MoneyTracker_dev.db");
#else
            options.UseSqlite("Data Source=MoneyTracker.db");
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
