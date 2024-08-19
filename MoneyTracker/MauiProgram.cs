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
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        #region Pages
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<CreateUserPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<JobPage>();
        builder.Services.AddTransient<LoanPage>();
        builder.Services.AddTransient<PasswordResetPage>();
        builder.Services.AddTransient<TransactionsPage>();
        builder.Services.AddTransient<UserPage>();
        #endregion
        #region ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<AutoPayViewModel>();
        builder.Services.AddTransient<CreateUserViewModel>();
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<JobViewModel>();
        builder.Services.AddTransient<LoanViewModel>();
        builder.Services.AddTransient<SavingsBucketsViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<TransactionTypeViewModel>();
        builder.Services.AddTransient<TransactionsViewModel>();
        builder.Services.AddTransient<UserViewModel>();
        #endregion
        #region Methods
        builder.Services.AddTransient<Helper>();
        #endregion
        #region Interfaces
        builder.Services.AddTransient<IDialogService, DialogService>();
        builder.Services.AddTransient<IServiceProvider, ServiceProvider>();
        #endregion

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {

#if DEBUG
            options.UseSqlite("Data Source=MoneyTracker_dev.db");
#else
            options.UseSqlite("Data Source=MoneyTracker.db");
#endif
        });

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
