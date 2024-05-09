using CommunityToolkit.Mvvm.ComponentModel;

namespace MoneyTracker.ViewModels
{
    public partial class AutoPayViewModel : ObservableObject
    {
        [ObservableProperty]
        bool autoPayActive;

        [ObservableProperty]
        int autopayDay;

        public DateTime? LastAutopayDate => new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, AutopayDay);

        public DateTime? NextAutoPayDate => new DateTime(DateTime.Today.Year, DateTime.Today.Month, AutopayDay);

        public TransactionTypeViewModel? TransactionType { get; set; } 
    }
}