using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.ViewModels
{
    [Table("FinanceAccounts")]
    public partial class AccountViewModel : ObservableObject
    {
        [ForeignKey("UserViewModel")]
        [Key]
        public int Id { get; set; } = 0;
        public virtual UserViewModel? User { get; set; }
        [ObservableProperty]
        string accountName = "Savings Default";
    }
}
