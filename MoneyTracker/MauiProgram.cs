using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyTracker.Pages;
using MoneyTracker.ViewModels;

namespace MoneyTracker
{
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

            builder.Services.AddDbContext<Context>(options => options.UseSqlite("Data Source=MoneyTracker.db"));

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ForgotUsernameOrPasswordPage>();
            builder.Services.AddTransient<CreateUserPage>();
            builder.Services.AddTransient<UserViewModel>();
            builder.Services.AddTransient<TransactionsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
