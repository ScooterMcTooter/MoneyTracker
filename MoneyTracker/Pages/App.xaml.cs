using Microsoft.EntityFrameworkCore;

namespace MoneyTracker
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            App.Current.UserAppTheme = App.Current.UserAppTheme == AppTheme.Dark || App.Current.UserAppTheme == AppTheme.Unspecified ? AppTheme.Dark : AppTheme.Light;

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MoneyTrackerMigrations.ApplicationDbContext>();
                db.Database.Migrate();
            }
            DependencyService.Register<ViewModels.SettingsViewModel>();
            var viewModel = DependencyService.Get<ViewModels.SettingsViewModel>();
            MainPage = new AppShell(viewModel);
        }

    }
}
