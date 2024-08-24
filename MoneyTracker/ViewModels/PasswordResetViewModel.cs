using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MoneyTracker.ViewModels;

[NotMapped]
public partial class PasswordResetViewModel : ObservableValidator
{
    private readonly ApplicationDbContext _db;
    public PasswordResetViewModel(ApplicationDbContext db)
    {
        _db = db;

        CanSubmit = false;
    }

    [ObservableProperty]
    string verificationCode = string.Empty;
    [ObservableProperty]
    string email = string.Empty;
    [ObservableProperty]
    bool canSubmit = false;



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

    private string? _newPassword;
    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordReq)]
    public string? NewPassword
    {
        get => _newPassword;
        set
        {
            SetProperty(ref _newPassword, value);
            ValidateProperty(value);

        }
    }

    private string? _repeatNewPassword;
    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordReq)]
    public string? RepeatNewPassword
    {
        get => _repeatNewPassword;
        set
        {
            if (NewPassword != null && value != NewPassword.Substring(0, value?.Length ?? 0))
            {
                // Display an error message
                Shell.Current.DisplayAlert("Passwords Do Not Match", "The new passwords do not match", "OK");
                return;
            }

            SetProperty(ref _repeatNewPassword, value);
            ValidateProperty(value);
        }
    }

    private bool IsValidPassword(string password)
    {
        string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{10,20}$";
        return Regex.IsMatch(password, pattern);
    }

    private string HashPassword(string? p)
    {
        if (string.IsNullOrEmpty(p))
        {
            throw new ArgumentNullException(nameof(p), "Password cannot be null or empty");
        }

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
        if (HashPassword(Password) != _password)
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
    async Task Send()
    {
        if (string.IsNullOrEmpty(Email))
        {
            // Display an error message or perform any other necessary action
            await Shell.Current.DisplayAlert("Invalid Input", "Please enter an email", "OK");
            return;
        }

        MailAddress to = new MailAddress(Email);
        MailAddress from = new MailAddress(Constants.SentFromEmail);

        MailMessage email = new MailMessage(from, to);
        email.Subject = "Money Tracker Verification";
        email.Body = $"Your verification code is: {GenerateVerificationCode()}";

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 465;
        smtp.Credentials = new NetworkCredential("MoneyTrackerScoot@gmail.com", "mG|AJul%$2q`juTQgGB_");
        smtp.EnableSsl = true;
        smtp.EnableSsl = true;

        try
        {
            
            smtp.Send(email);
            CanSubmit = true;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    string GenerateVerificationCode()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string verify = new string(Enumerable.Repeat(chars, 6)
                     .Select(s => s[random.Next(s.Length)]).ToArray());

        _db.verificationModels.Add(new VerificationModel
        {
            Email = Email,
            VerificationCode = verify,
            Expiration = DateTime.Now.AddMinutes(15),
            Used = false,
            Expired = false,
            Verified = false
        });

        return verify;
    }
}