namespace MoneyTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            MainPage = new AppShell();
        }
    }
}
