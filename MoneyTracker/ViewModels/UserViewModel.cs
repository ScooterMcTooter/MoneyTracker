using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Pages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // Use regular expression to validate phone number format
        // This regular expression pattern checks for a basic phone number format
        // You can modify it to fit your specific requirements
        string pattern = @"^\(\d{3}\) \d{3}-\d{4}$";
        return Regex.IsMatch(phoneNumber, pattern);
    }

    private bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{10,20}$";
        return Regex.IsMatch(password, pattern);
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

    private DateTime? _dob;
    public DateTime? Dob
    {
        get => _dob;
        set
        {
            if (value.HasValue && value.Value > DateTime.Now)
            {
                // notify the user that it cannot be a future date
                Shell.Current.DisplayAlert("Invalid Date", "Date of birth cannot be in the future", "OK");
            }
            else
            {
                SetProperty(ref _dob, value);
            }
        }
    }

    public List<LoansViewModel>? Loans { get; set; }

    [ForeignKey("AutoPayViewModel")]
    public virtual List<AutoPayViewModel>? AutoPays { get; set; }

    [ForeignKey("AccountViewModel")]
    public virtual List<AccountViewModel>? Accounts { get; set; }

    [ForeignKey("SavingsBucketsViewModel")]
    public virtual List<SavingsBucketsViewModel>? SavingsBuckets { get; set; }

    [ObservableProperty]
    string? currentPassword;
    [ObservableProperty]
    string? newPassword;
    [ObservableProperty]
    string? repeatNewPassword;
    [ObservableProperty]
    bool isChangePasswordVisible;
    [ObservableProperty]
    bool isCreateUserVisible;

    private readonly ApplicationDbContext _db;

    public UserViewModel(ApplicationDbContext db)
    {
        _db = db;
    }

    private string HashPassword(string? p)
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
    async Task ChangePassword()
    {
        if (HashPassword(CurrentPassword) != _password)
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

        _password = HashPassword(NewPassword);
    }

    [RelayCommand]
    async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(CreateUserPage));
    }

    [RelayCommand]
    async Task CreateUser()
    {
        try
        {
            if (string.IsNullOrEmpty(PhoneNumber) || !IsValidPhoneNumber(PhoneNumber))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Invalid Phone Number", "Please enter a valid phone number", "OK");
                return;
            }

            // Validate the email address before proceeding with the creation process
            if (string.IsNullOrEmpty(UserEmail) || !new EmailAddressAttribute().IsValid(UserEmail))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Invalid Email", "Please enter a valid email address", "OK");
                return;
            }

            // Validate the password before proceeding with the creation process
            if (string.IsNullOrEmpty(Password) || !IsValidPassword(Password))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Invalid Password", Constants.PasswordReq, "OK");
                return;
            }

            // Check if the username already exists
            if (await _db.userViewModels.AnyAsync(u => u.Username == Username))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Username Exists", "The username already exists", "OK");
                return;
            }

            // Check if the email already exists
            if (await _db.userViewModels.AnyAsync(u => u.UserEmail == UserEmail))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Email Exists", "The email already exists", "OK");
                return;
            }

            // Check if the phone number already exists
            if (await _db.userViewModels.AnyAsync(u => u.PhoneNumber == PhoneNumber))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Phone Number Exists", "The phone number already exists", "OK");
                return;
            }

            // Hash the password before saving it to the database
            string hashedPassword = HashPassword(Password ?? "changeme123");

            // Create a new user
            var user = new UserViewModel(_db)
            {
                FirstName = FirstName,
                LastName = LastName,
                Suffix = Suffix,
                Username = Username,
                Password = hashedPassword,
                UserEmail = UserEmail,
                PhoneNumber = PhoneNumber,
                Dob = Dob
            };

            // Save the new user to the database
            _db.userViewModels.Add(user);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unknown Exception Occured", $"StackTrace:{Environment.NewLine}{ex.StackTrace}", "Ok");
            throw;
        }

        // Proceed to the home page
        await Shell.Current.GoToAsync(nameof(HomePage));
    }

    [RelayCommand]
    async Task Login()
    {
        if (_password == "changeme123")
        {
            // The password does not meet the requirements
            await ChangeVisablity();
            return;
        }

        string hashedPassword = HashPassword(_password);
        var user = await _db.userViewModels.FirstOrDefaultAsync(u => u.Username == Username && u.Password == hashedPassword);

        if (user != null)
        {
            Constants.IsAuthenticated = true;
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
        else
        {
            // The login failed
            // Create a notification that the login failed   
        }
    }

    //take me to the Account page
    [RelayCommand]
    async Task Account()
    {
        await Shell.Current.GoToAsync(nameof(AccountPage));
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

    [RelayCommand]
    async Task ChangeVisablity()
    {
        IsChangePasswordVisible = !IsChangePasswordVisible;
        IsCreateUserVisible = !IsCreateUserVisible;
        await Shell.Current.GoToAsync(nameof(CreateUserPage));
    }

    async Task Logout()
    {
        Constants.IsAuthenticated = false;
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}
