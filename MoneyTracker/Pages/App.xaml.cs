using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoneyTracker.Pages;
using MoneyTrackerMigrations;

namespace MoneyTracker
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            if (App.Current != null)
                App.Current.UserAppTheme = App.Current.UserAppTheme == AppTheme.Dark || App.Current.UserAppTheme == AppTheme.Unspecified ? AppTheme.Dark : AppTheme.Light;

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MoneyTrackerMigrations.ApplicationDbContext>();
                db.Database.Migrate();
            }

            var myParameter = serviceProvider.GetService<ApplicationDbContext>();
            var serviceCollection = serviceProvider.GetService<IServiceCollection>();

            if (serviceCollection != null && myParameter != null)
                serviceCollection.AddTransient(provider => new ViewModels.LoginViewModel(myParameter));

            var viewModel = serviceProvider.GetService<ViewModels.SettingsViewModel>();
            if (viewModel != null && myParameter != null)
                MainPage = new AppShell(serviceProvider, viewModel);
        }

    }
}
