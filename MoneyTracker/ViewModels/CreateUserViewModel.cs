using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using MoneyTrackerMigrations.Models;
using MoneyTracker.Pages;
using Microsoft.EntityFrameworkCore;

namespace MoneyTracker.ViewModels;

public partial class CreateUserViewModel
{
    private readonly ApplicationDbContext _db;
    public CreateUserViewModel(ApplicationDbContext db)
    {
        _db = db;
    }

    UserModel user = new UserModel();

    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Suffix { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordReq)]
    public string Password { get; set; } = "ChangeMe@123";

    [Required]
    [PasswordPropertyText]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{15,20}$", ErrorMessage = Constants.PasswordMissmatch)]
    public string? ConfirmPassword { get; set; }
    public int PhoneNumber { get; set; }
    public DateTime Dob { get; set; }
    public virtual UserViewModel? User { get; set; }
    public virtual LoansViewModel? Loans { get; set; }
    public virtual AccountViewModel? Account { get; set; }
    public virtual TransactionsViewModel? Transactions { get; set; }
    public virtual AutoPayViewModel? AutoPay { get; set; }

    /// <summary>
    /// Validates the phone number format.
    /// </summary>
    /// <param name="phoneNumber">The phone number to validate.</param>
    /// <returns>True if the phone number is valid, false otherwise.</returns>
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return int.TryParse(phoneNumber, out int result);
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
    /// Creates a new user.
    /// </summary>
    [RelayCommand]
    async Task CreateUser()
    {
        try
        {

            if (string.IsNullOrEmpty(PhoneNumber.ToString()))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Invalid Phone Number", "Please enter a valid phone number", "OK");
                return;
            }

            // Validate the email address before proceeding with the creation process
            if (string.IsNullOrEmpty(Email) || !new EmailAddressAttribute().IsValid(Email))
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
            if (_db.userModels.Any(u => u.UserName == Username))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Username Exists", "The username already exists", "OK");
                return;
            }

            // Check if the email already exists
            if (_db.userModels.Any(u => u.Email == Email))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Email Exists", "The email already exists", "OK");
                return;
            }

            // Check if the phone number already exists
            if (_db.userModels.Any(u => u.PhoneNumber == PhoneNumber))
            {
                // Display an error message or perform any other necessary action
                await Shell.Current.DisplayAlert("Phone Number Exists", "The phone number already exists", "OK");
                return;
            }

            // Hash the password before saving it to the database
            string hashedPassword = Password == null ? "changeme123" : new Helper().HashPassword(Password);

            // Create a new user
            user = new UserModel()
            {
                FirstName = FirstName,
                LastName = LastName,
                Suffix = Suffix,
                UserName = Username,
                Password = hashedPassword,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Dob = Dob
            };

            // Save the new user to the database
            _db.userModels.Add(user);
            _db.SaveChanges();

            // Get the Id for the user that was just created
            var newUser = _db.userModels.FirstOrDefault(u => u.UserName == Username);
        }
        catch (DbUpdateException ex)
        {
            await Shell.Current.DisplayAlert("Update Error", $"Exception:{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}", "Ok");
            return;
        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Unknown Exception Occured", $"Exception:{Environment.NewLine}{ex}", "Ok");
            return;
        }

        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}
