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

            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            var serviceCollection = serviceProvider.GetService<IServiceCollection>();
            var userViewModel = serviceProvider.GetService<ViewModels.UserViewModel>();
            var LoanViewModel = serviceProvider.GetService<ViewModels.LoanViewModel>();
            var JobViewModel = serviceProvider.GetService<ViewModels.JobViewModel>();
            var CreateUserViewModel = serviceProvider.GetService<ViewModels.CreateUserViewModel>();
            var SavingsBucketsViewModel = serviceProvider.GetService<ViewModels.SavingsBucketsViewModel>();
            var SettingsViewModel = serviceProvider.GetService<ViewModels.SettingsViewModel>();
            var AutoPayViewModel = serviceProvider.GetService<ViewModels.AutoPayViewModel>();
            var TransactionsViewModel = serviceProvider.GetService<ViewModels.TransactionsViewModel>();

            if (serviceCollection != null && dbContext != null)
            {
                serviceCollection.AddTransient(provider => new ViewModels.LoginViewModel(dbContext));
                serviceCollection.AddTransient(provider => new ViewModels.HomeViewModel(dbContext));
                serviceCollection.AddTransient(provider => new ViewModels.UserViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.AccountViewModel(myParameter));
                serviceCollection.AddTransient(provider => new ViewModels.TransactionsViewModel(dbContext, userViewModel));
                serviceCollection.AddTransient(provider => new ViewModels.JobViewModel(provider));
                serviceCollection.AddTransient(provider => new ViewModels.LoanViewModel(provider));
                serviceCollection.AddTransient(provider => new ViewModels.CreateUserViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.SavingsBucketsViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.SettingsViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.AutoPayViewModel(dbContext));

            }

            var viewModel = serviceProvider.GetService<ViewModels.SettingsViewModel>();
            if (viewModel != null && dbContext != null)
                MainPage = new AppShell(serviceProvider, viewModel);
        }

    }
}
