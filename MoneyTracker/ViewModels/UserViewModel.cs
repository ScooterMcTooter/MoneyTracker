using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Pages;
using MoneyTrackerMigrations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace MoneyTracker.ViewModels;

/// <summary>
/// ViewModel for handling user-related operations.
/// </summary>
public partial class UserViewModel : ObservableValidator
{
    /// <summary>
    /// Gets or sets the user's ID.
    /// </summary>
    private int _id;
    [Key]
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    private string? _phoneNumber;
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = new string(value.Where(char.IsDigit).ToArray());

                if (value.Length == 10)
                {
                    value = string.Format("{0:(###) ###-####}", double.Parse(value));
                }
            }

            SetProperty(ref _phoneNumber, value);
        }
    }

    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    private string? _userEmail;
    public string? UserEmail
    {
        get => _userEmail;
        set
        {
            Console.WriteLine($"Setting Email to '{value}'");
            SetProperty(ref _userEmail, value);
        }
    }

    /// <summary>
    /// Validates the phone number format.
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns>True if the phone number is valid, false otherwise.</returns>
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^\(\d{3}\) \d{3}-\d{4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    /// <summary>
    /// Validates the password format.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <returns>True if the password is valid, false otherwise.</returns>
    private bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{10,20}$";
        return Regex.IsMatch(password, pattern);
    }

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    private string? _firstName = Environment.UserName ?? string.Empty;
    [Required]
    public string? FirstName
    {
        get => _firstName;
        set
        {
            SetProperty(ref _firstName, value);
            ValidateProperty(value);
        }
    }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    private string? _lastName;
    [Required]
    public string? LastName
    {
        get => _lastName;
        set
        {
            SetProperty(ref _lastName, value);
            ValidateProperty(value);
        }
    }

    /// <summary>
    /// Gets or sets the user's suffix.
    /// </summary>
    private string? _suffix;
    public string? Suffix
    {
        get => _suffix;
        set => SetProperty(ref _suffix, value);
    }

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    private string? _password;
    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordReq)]
    public string? Password
    {
        get => _password;
        set
        {
            if (value != null)
            {
                value = value.Trim().Replace(" ", string.Empty);
            }
            SetProperty(ref _password, value);
            ValidateProperty(value);
        }
    }

    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    private string? _username;
    [Required]
    public string? Username
    {
        get => _username;
        set
        {
            if (value != null)
            {
                value = value.Trim().Replace(" ", string.Empty);
            }
            SetProperty(ref _username, value);
            ValidateProperty(value);
        }
    }

    /// <summary>
    /// Gets or sets the user's date of birth.
    /// </summary>
    private DateTime? _dob;
    public DateTime? Dob
    {
        get => _dob;
        set
        {
            if (value.HasValue)
            {
                if (value.Value > DateTime.Now)
                {
                    Shell.Current.DisplayAlert("Invalid Date", "Date of birth cannot be in the future", "OK");
                }

                if ((DateTime.Now.Year - value.Value.Year) < 18)
                {
                    Shell.Current.DisplayAlert("Invalid Date", "You have to be at least 18 to use this app.", "OK");
                }

                SetProperty(ref _dob, value);
            }
        }
    }

    /// <summary>
    /// Gets or sets the user's loans.
    /// </summary>
    public List<LoanViewModel>? Loans { get; set; }

    /// <summary>
    /// Gets or sets the user's auto pays.
    /// </summary>
    [ForeignKey("AutoPayViewModel")]
    public virtual List<AutoPayViewModel>? AutoPays { get; set; }

    /// <summary>
    /// Gets or sets the user's accounts.
    /// </summary>
    [ForeignKey("AccountViewModel")]
    public virtual List<AccountViewModel>? Accounts { get; set; }

    /// <summary>
    /// Gets or sets the user's savings buckets.
    /// </summary>
    [ForeignKey("SavingsBucketsViewModel")]
    public virtual List<SavingsBucketsViewModel>? SavingsBuckets { get; set; }

    /// <summary>
    /// Gets or sets the current password.
    /// </summary>
    [ObservableProperty]
    string? currentPassword;

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    [ObservableProperty]
    string? newPassword;

    /// <summary>
    /// Gets or sets the repeated new password.
    /// </summary>
    [ObservableProperty]
    string? repeatNewPassword;

    /// <summary>
    /// Gets or sets the visibility of the change password.
    /// </summary>
    [ObservableProperty]
    bool isChangePasswordVisible;

    /// <summary>
    /// Gets or sets the visibility of the create user.
    /// </summary>
    [ObservableProperty]
    bool isCreateUserVisible;

    /// <summary>
    /// The database context.
    /// </summary>
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserViewModel"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    public UserViewModel(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Changes the password.
    /// </summary>
    [RelayCommand]
    async Task ChangePassword()
    {
        try
        {
            if (CurrentPassword != _password)
            {
                // The current password is incorrect
                // Display an error message
                await Shell.Current.DisplayAlert("Invalid Password", "The current password was incorrect", "OK");
                return;
            }

            if (NewPassword != RepeatNewPassword)
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Passwords Do Not Match", "The new passwords do not match", "OK");
                return;
            }

            _password = new Helper().HashPassword(NewPassword);
            var user = await _db.userModels.FirstOrDefaultAsync(u => u.Id == Id);
            if (user != null)
            {
                user.Password = _password;
                await _db.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to update password: {ex}", "OK");
        }

    }

    /// <summary>
    /// Navigates to the account page.
    /// </summary>
    [RelayCommand]
    async Task Account()
    {
        await Shell.Current.GoToAsync(nameof(AccountPage));
    }

    /// <summary>
    /// Navigates to the password reset page.
    /// </summary>
    [RelayCommand]
    async Task Forgot(string username)
    {
        await Shell.Current.GoToAsync($"{nameof(PasswordResetPage)}?username={username}");
    }

    /// <summary>
    /// Logs out the user.
    /// </summary>
    [RelayCommand]
    async Task Logout()
    {
        Constants.IsAuthenticated = false;
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}