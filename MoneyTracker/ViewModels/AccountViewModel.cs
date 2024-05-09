using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels
{
    public partial class AccountViewModel : ObservableObject
    {
        [Key]
        public int Id { get; set; } = 0;
        [ObservableProperty]
        string accountName = "Savings Default";
    }
}
