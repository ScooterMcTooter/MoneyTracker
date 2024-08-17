using Microsoft.EntityFrameworkCore;
using MoneyTracker.ViewModels;

namespace MoneyTracker
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ApplicationDbContext _db;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _db = _serviceProvider.GetService<ApplicationDbContext>() ?? throw new NotImplementedException("There is a failure when trying to access the ApplicationDbContext service.");

            if (App.Current != null)
                App.Current.UserAppTheme = App.Current.UserAppTheme == AppTheme.Dark || App.Current.UserAppTheme == AppTheme.Unspecified ? AppTheme.Dark : AppTheme.Light;

            var dbContext = serviceProvider.GetService<ApplicationDbContext>();
            var Helper = serviceProvider.GetService<Helper>();
            var serviceCollection = serviceProvider.GetService<IServiceCollection>();
            var AutoPayViewModel = serviceProvider.GetService<AutoPayViewModel>();
            var AccountViewModel = serviceProvider.GetService<AccountViewModel>();
            var CreateUserViewModel = serviceProvider.GetService<CreateUserViewModel>();
            var JobViewModel = serviceProvider.GetService<JobViewModel>();
            var LoanViewModel = serviceProvider.GetService<LoanViewModel>();
            var SavingsBucketsViewModel = serviceProvider.GetService<SavingsBucketsViewModel>();
            var SettingsViewModel = serviceProvider.GetService<SettingsViewModel>();
            var TransactionsViewModel = serviceProvider.GetService<TransactionsViewModel>();
            var UserViewModel = serviceProvider.GetService<UserViewModel>();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            if (_db != null)
                MainPage = new AppShell(serviceProvider);

            if (serviceCollection != null && _db != null)
            {
                serviceCollection.AddTransient(provider => new Helper());
                serviceCollection.AddTransient(provider => new AccountViewModel(_serviceProvider));
                serviceCollection.AddTransient(provider => new HomeViewModel(_db, _serviceProvider));
                serviceCollection.AddTransient(provider => new JobViewModel(_serviceProvider));
                serviceCollection.AddTransient(provider => new LoanViewModel(_serviceProvider));
                serviceCollection.AddTransient(provider => new LoginViewModel(_db));
                serviceCollection.AddTransient(provider => new TransactionsViewModel(_db, _serviceProvider.GetService<UserViewModel>() ?? new UserViewModel(_db)));
                serviceCollection.AddTransient(provider => new UserViewModel(_db));
                serviceCollection.AddTransient(provider => new CreateUserViewModel(_db));
                //serviceCollection.AddTransient(provider => new AutoPayViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.SavingsBucketsViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.SettingsViewModel(dbContext));
            }
        }
    }
}
