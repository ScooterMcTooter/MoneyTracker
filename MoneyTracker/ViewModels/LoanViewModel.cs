using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.ViewModels
{
    public partial class LoanViewModel : ObservableObject
    {
        [ObservableProperty]
        double loanAmount;
        [ObservableProperty]
        double totalInterest;
        [ObservableProperty]
        double interestRate;
        [ObservableProperty]
        double monthlyPayment; 
        [ObservableProperty]
        double totalPayment;
        [ObservableProperty]
        int? loanTerm = null;
        [ObservableProperty]
        string? loanType;
        [ObservableProperty]
        string? loanName;
    }
}
