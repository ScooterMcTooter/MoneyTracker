using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyTrackerMigrations.Models;
using MoneyTracker.Pages;
using MoneyTrackerMigrations;

namespace MoneyTracker.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    /// <summary>
    /// The database context.
    /// </summary>
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    public LoginViewModel(ApplicationDbContext db)
    {
        _db = db;
    }

    private string _username = string.Empty;
    private string _password = string.Empty;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    /// <summary>
    /// Navigates to the create user page.
    /// </summary>
    [RelayCommand]
    async Task Create()
    {
        try
        { 
        await Shell.Current.GoToAsync(nameof(CreateUserPage));
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync();
        }
    }

    /// <summary>
    /// Logs in the user.
    /// </summary>
    [RelayCommand]
    async Task Login()
    {
        try
        {
            string pass;
            if (_password == "changeme123")
            {
                return;
            }
            else if (string.IsNullOrEmpty(_password))
            {
                await Shell.Current.DisplayAlert("Invalid Password", "Password field empty, please enter a password", "OK");
                return;
            }
            else
            {
                pass = new Helper().HashPassword(_password);
            }

            UserModel? user = _db.userModels.First(u => u.UserName != null && u.Password != null && u.UserName.Equals(_username) && u.Password.Equals(pass));

            if (user != null)
            {
                Constants.IsAuthenticated = true;
                Constants.CurrentUser = user;
                if (user != null)
                {
                    Constants.ConstSettings = _db.settingsModels.Any(s => s.UserId == user.Id) ? _db.settingsModels.FirstOrDefault(s => s.UserId == user.Id) ?? new SettingsModel() : new SettingsModel();
                    Constants.ConstAccounts = _db.accountModels.Any(a => a.UserId == user.Id) ? _db.accountModels.Where(a => a.UserId == user.Id).ToList() : new List<AccountModel>();
                    Constants.ConstAutoPays = _db.autoPayModels.Any(a => a.UserId == user.Id) ? _db.autoPayModels.Where(a => a.UserId == user.Id).ToList() : new List<AutoPayModel>();
                    Constants.ConstJobs = _db.jobModels.Any(j => j.UserId == user.Id) ? _db.jobModels.Where(j => j.UserId == user.Id).ToList() : new List<JobModel>();
                    Constants.ConstLoans = _db.loanModels.Any(l => l.UserId == user.Id) ? _db.loanModels.Where(l => l.UserId == user.Id).ToList() : new List<LoanModel>();
                    Constants.ConstTransactions = _db.transactionModels.Any(t => t.UserId == user.Id) ? _db.transactionModels.Where(t => t.UserId == user.Id).ToList() : new List<TransactionModel>();
                    //Constants.ConstBuckets = _db.bucketModels.Any(b => Constants.ConstAccounts.Any(a => a.Id == b.AccountId)) ? _db.bucketModels.Where(b => Constants.ConstAccounts.Any(a => a.Id == b.AccountId)).ToList() : new List<BucketModel>();
                    Constants.maxWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
                }

                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                // The login failed
                // Create a notification that the login failed
                await Shell.Current.DisplayAlert("Login Failed", "The username or password is incorrect", "OK");
                return;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to login: {ex}", "OK");
        }
    }
}
