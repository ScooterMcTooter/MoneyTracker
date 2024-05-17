using Microsoft.EntityFrameworkCore;

namespace MoneyTracker
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MoneyTrackerMigrations.ApplicationDbContext>();
                db.Database.Migrate();
            }

            MainPage = new AppShell(new ViewModels.SettingsViewModel());
        }

    }
}
