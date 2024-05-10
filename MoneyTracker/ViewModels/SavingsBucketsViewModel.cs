using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.ViewModels
{
     public partial class SavingsBucketsViewModel : ObservableObject
    {
        [Key]
        public int Id { get; set; }
        [ObservableProperty]
        string name = "new";
        [ObservableProperty]
        double targetAmount;
        [ObservableProperty]
        double currentAmount;
        DateTime UpdateDate { get; set; } = DateTime.Now;
        [ObservableProperty]
        string plannedUse = "Vacation";

        [ForeignKey("UserViewModel")]
        public int UserId { get; set; }
        public virtual List<UserViewModel>? Users { get; set; }
    }
}
