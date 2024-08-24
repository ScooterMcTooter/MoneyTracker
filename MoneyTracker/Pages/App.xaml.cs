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

            var dbContext = _serviceProvider.GetService<ApplicationDbContext>() ?? null!;
            var Helper = _serviceProvider.GetService<Helper>() ?? null!;
            var serviceCollection = _serviceProvider.GetService<IServiceCollection>() ?? null!;
            var AutoPayViewModel = _serviceProvider.GetService<AutoPayViewModel>() ?? null!;
            var AccountViewModel = _serviceProvider.GetService<AccountViewModel>() ?? null!;
            var CreateUserViewModel = _serviceProvider.GetService<CreateUserViewModel>() ?? null!;
            var JobViewModel = _serviceProvider.GetService<JobViewModel>() ?? null!;
            var LoanViewModel = _serviceProvider.GetService<LoanViewModel>() ?? null!;
            var SavingsBucketsViewModel = _serviceProvider.GetService<SavingsBucketsViewModel>() ?? null!;
            var SettingsViewModel = _serviceProvider.GetService<SettingsViewModel>() ?? null!;
            var TransactionsViewModel = _serviceProvider.GetService<TransactionsViewModel>() ?? null!;
            var UserViewModel = _serviceProvider.GetService<UserViewModel>() ?? null!;
            var LoginViewModel = _serviceProvider.GetService<LoginViewModel>() ?? null!;
            var PasswordResetViewModel = _serviceProvider.GetService<PasswordResetViewModel>() ?? null!;

            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            if (dbContext != null)
                MainPage = new AppShell(_serviceProvider);

            if (serviceCollection != null && dbContext != null)
            {
                serviceCollection.AddTransient(provider => new Helper());
                serviceCollection.AddSingleton(provider => new AccountViewModel(_serviceProvider));
                serviceCollection.AddSingleton(provider => new HomeViewModel(dbContext, _serviceProvider));
                serviceCollection.AddSingleton(provider => new JobViewModel(_serviceProvider));
                serviceCollection.AddSingleton(provider => new LoanViewModel(_serviceProvider));
                serviceCollection.AddSingleton(provider => new LoginViewModel(dbContext));
                serviceCollection.AddSingleton(provider => new TransactionsViewModel(dbContext, UserViewModel));
                serviceCollection.AddSingleton(provider => new UserViewModel(dbContext));
                serviceCollection.AddSingleton(provider => new CreateUserViewModel(dbContext));
                serviceCollection.AddSingleton(provider => new PasswordResetViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new AutoPayViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.SavingsBucketsViewModel(dbContext));
                //serviceCollection.AddTransient(provider => new ViewModels.SettingsViewModel(dbContext));
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
#if !DEBUG
            try
            {
                _serviceProvider.GetService<LoginViewModel>().Password = "AcerAspireR7!1995";
                _serviceProvider.GetService<LoginViewModel>().Username = "Scoot";
                _serviceProvider.GetService<LoginViewModel>().Login();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Shell.Current.GoToAsync("//HomePage");
            }
#else
            Shell.Current.GoToAsync("//LoginPage");
#endif
        }
    }
}
