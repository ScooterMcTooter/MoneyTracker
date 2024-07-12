using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyTracker.Pages;
using MoneyTracker.ViewModels;
using MoneyTrackerMigrations;

namespace MoneyTracker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit() // Added the missing method call
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        builder.Services.AddSingleton<AccountPage>();
        builder.Services.AddSingleton<CreateUserPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<JobPage>();
        builder.Services.AddSingleton<LoanPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<PasswordResetPage>();
        builder.Services.AddSingleton<TransactionsPage>();
        builder.Services.AddSingleton<UserPage>();

        builder.Services.AddSingleton<AccountViewModel>();
        builder.Services.AddSingleton<AutoPayViewModel>();
        builder.Services.AddSingleton<CreateUserViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<JobViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoanViewModel>();
        builder.Services.AddSingleton<SavingsBucketsViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<TransactionTypeViewModel>();
        builder.Services.AddSingleton<TransactionsViewModel>();
        builder.Services.AddSingleton<UserViewModel>();
        builder.Services.AddSingleton<IServiceProvider, ServiceProvider>();
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
