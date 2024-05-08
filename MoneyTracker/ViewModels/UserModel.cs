using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels
{
    public partial class UserModel : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string FirstName { get; set; } = Environment.UserName ?? string.Empty;
        [Required]
        [StringLength(30)]
        public string? LastName { get; set; }
        [StringLength(5)]
        public string? Suffix { get; set; }
        [PasswordPropertyText]
        [ObservableProperty]
        string? password;
        [ObservableProperty]
        string? username;
    }
}
