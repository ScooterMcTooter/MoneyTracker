using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MoneyTracker.ViewModels
{
    [NotMapped]
    public partial class PasswordResetViewModel : ObservableValidator
    {
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
                //Check that it is matching NewPassword
                if (NewPassword != null && value != NewPassword.Substring(0, value?.Length ?? 0))
                {
                    //Display an error message
                    Shell.Current.DisplayAlert("Passwords Do Not Match", "The new passwords do not match", "OK");
                    //make an element visible
                    //isChangePasswordVisible = true;
                    return;
                }


                //SetProperty(ref _repeatNewPassword, value);
                //ValidateProperty(value);

            }
        }

        private bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{10,20}$";
            return Regex.IsMatch(password, pattern);
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
    }
}
