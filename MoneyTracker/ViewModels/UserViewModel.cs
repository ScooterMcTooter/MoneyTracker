using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Pages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MoneyTracker.ViewModels;

public partial class UserViewModel : ObservableValidator
{
    [Key]
    public int Id { get; set; }

    private string? _phoneNumber;
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (!string.IsNullOrEmpty(value) && IsValidPhoneNumber(value))
            {

            }
            else
                SetProperty(ref _phoneNumber, value);
        }
    }

    private string? _email;

    public string? email
    {
        get => _email;
        set
        {
            if (!string.IsNullOrEmpty(value) && !IsValidEmail(value))
            {
                // Handle invalid email format
                // You can display an error message or perform any other necessary action
            }
            else
            {
                SetProperty(ref _email, value);
            }
        }
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // Use regular expression to validate phone number format
        // This regular expression pattern checks for a basic phone number format
        // You can modify it to fit your specific requirements
        string pattern = @"^\d{3}-\d{3}-\d{4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    private bool IsValidEmail(string email)
    {
        // Use regular expression to validate email format
        // This regular expression pattern checks for a basic email format
        // You can modify it to fit your specific requirements
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }


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

    private string? _suffix;
    public string? Suffix
    {
        get => _suffix;
        set => SetProperty(ref _suffix, value);
    }

    private string? _password;
    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordReq)]
    public string? Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            ValidateProperty(value);
        }
    }

    private string? _username;
    [Required]
    public string? Username
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            ValidateProperty(value);
        }
    }

    private readonly Context _db;

    public UserViewModel(Context db)
    {
        _db = db;
    }

    private string HashPassword(string p)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(p));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    [RelayCommand]
    async void Login()
    {
        string hashedPassword = HashPassword(_password ?? "changeme123");
        Constants.IsAuthenticated = await _db.userViewModels.AnyAsync(u => u.Username == Username && u.Password == hashedPassword);

        if (Constants.IsAuthenticated)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
        else
        {
            // The login failed
            // Create a notification that the login failed   
        }
    }

    //take me to the create page
    [RelayCommand]
    async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(CreateUserPage));
    }

    //take me to the forgot username or password page
    [RelayCommand]
    async Task Forget(string user)
    {
        await Shell.Current.GoToAsync(nameof(ForgotUsernameOrPasswordPage), new Dictionary<string, object>
        {
            { nameof(ForgotUsernameOrPasswordPage), user }
        });
    }
}
