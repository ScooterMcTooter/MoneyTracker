using Microsoft.EntityFrameworkCore;

namespace MoneyTracker
{
    public partial class App : Application
    {
        private readonly ApplicationDbContext _db;

        public App(ApplicationDbContext db)
        {
            using (_db = db)
            {
                InitializeComponent();

                _db.Database.Migrate();

                MainPage = new AppShell();
            }
        }
    }
}
