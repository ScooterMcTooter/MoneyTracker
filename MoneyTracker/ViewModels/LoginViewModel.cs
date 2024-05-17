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
        await Shell.Current.GoToAsync(nameof(CreateUserPage));
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

            UserModel? user = _db.userModels.FirstOrDefault(u => u.UserName != null && u.Password != null && u.UserName.Equals(_username) && u.Password.Equals(pass));

            if (user != null)
            {
                Constants.IsAuthenticated = true;
                await Shell.Current.GoToAsync(nameof(HomePage));
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
