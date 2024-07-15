using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTrackerMigrations;
using MoneyTrackerMigrations.Models;
using System.Collections.ObjectModel;
using Xamarin.KotlinX.Coroutines;

namespace MoneyTracker.ViewModels;

public partial class LoanViewModel : ObservableObject
{
    private readonly ApplicationDbContext _db;
    private readonly IServiceProvider _serviceProvider;
    public LoanViewModel(IServiceProvider serviceProvider)
    {
        try
        {
            _serviceProvider = serviceProvider;
            _db = _serviceProvider.GetService<ApplicationDbContext>() ?? throw new InvalidOperationException("ApplicationDbContext could not be retrieved from the service provider.");
            
            Loans = new ObservableCollection<LoanModel>(Constants.ConstLoans);
            UserIdop = Constants.CurrentUser.Id;
            User = _db.userModels.Find(UserIdop);
            //UserId = User.Id;
            Transactions = new ObservableCollection<TransactionModel>(Constants.ConstTransactions);
            SelectedLoan = _db.loanModels.FirstOrDefault() ?? new LoanModel();
            IsStudentLoan = SelectedLoanType == LoanType.Student;
            RemainingBalance = selectedLoan.RemainingBalance.ToString();
            RemainingInterest = (SelectedLoan.Amount - SelectedLoan.TotalInterest).ToString();
            Guarantor = SelectedLoan.Guarantor;
            DisbursementDate = SelectedLoan.DisbursementDate;
            RepaymentPlan = Enum.Parse<LoanRepaymentPlans>(SelectedLoan.RepaymentPlan);
            LoanString = Constants.ConstLoans.Count() > 0 ? $"You have {Constants.ConstLoans.Count()} loan(s) with a total of ${Constants.ConstLoans.Sum(l => l.Amount)}" : "You have no active loans!";
            IsAddLoanVisible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Properties
    public List<string> LoanTypeValues
    {
        get
        {
            return Enum.GetValues(typeof(LoanType))
                       .Cast<LoanType>()
                       .Select(e => e.ToString())
                       .ToList();
        }
    }

    public List<string> LoanStatusValues
    {
        get
        {
            return Enum.GetValues(typeof(LoanStatus))
                       .Cast<LoanStatus>()
                       .Select(e => e.ToString())
                       .ToList();
        }
    }

    public List<string> StudentLoanTypeValues
    {
        get
        {
            return Enum.GetValues(typeof(StudentLoanType))
                       .Cast<StudentLoanType>()
                       .Select(e => e.ToString())
                       .ToList();
        }
    }

    public List<string> FedLoanTypeValues
    {
        get
        {
            return Enum.GetValues(typeof(FedLoanType))
                       .Cast<FedLoanType>()
                       .Select(e => e.ToString())
                       .ToList();
        }

    }

    public List<string> InterestTypeValues
    {
        get
        {
            return Enum.GetValues(typeof(InterestType))
                       .Cast<InterestType>()
                       .Select(e => e.ToString())
                       .ToList();
        }
    }

    public List<string> LoanRepaymentPlanValues
    {
        get
        {
            return Enum.GetValues(typeof(LoanRepaymentPlans))
                       .Cast<LoanRepaymentPlans>()
                       .Select(e => e.ToString())
                       .ToList();
        }
    }

    public string? TransactionString => Transactions?.Count() > 0 ? null : "No Previous Payments Available";
    #endregion

    #region Observable Properties
    [ObservableProperty]
    string addOrClose = "Add Loan";
    [ObservableProperty]
    bool edit = false;
    [ObservableProperty]
    string loanString;
    [ObservableProperty]
    bool isStudentLoan = false;
    [ObservableProperty]
    ObservableCollection<LoanModel> loans;
    [ObservableProperty]
    string? loanName;
    [ObservableProperty]
    double loanAmount;
    [ObservableProperty]
    double interestRate;
    [ObservableProperty]
    double totalInterest;
    [ObservableProperty]
    string monthlyPayment = string.Empty;
    [ObservableProperty]
    string remainingBalance;
    [ObservableProperty]
    string remainingInterest;
    [ObservableProperty]
    DateTime dueDate = DateTime.Now.AddDays(7).Date;
    [ObservableProperty]
    DateTime paymentDate = DateTime.Now.AddDays(6);
    [ObservableProperty]
    string? servicer;
    [ObservableProperty]
    bool isPaid = false;
    [ObservableProperty]
    bool paidOff = false;
    [ObservableProperty]
    int userIdop;
    [ObservableProperty]
    UserModel? user;
    [ObservableProperty]
    ObservableCollection<TransactionModel>? transactions;
    [ObservableProperty]
    TransactionModel? selectedTransaction;
    [ObservableProperty]
    int autoPayId;
    [ObservableProperty]
    ObservableCollection<AutoPayModel>? autoPay;
    [ObservableProperty]
    AutoPayModel? selectedAutoPay;
    [ObservableProperty]
    LoanModel selectedLoan;
    [ObservableProperty]
    bool isAddLoanVisible = false;
    [ObservableProperty]
    LoanType? selectedLoanType = LoanType.Student;
    [ObservableProperty]
    LoanType loanType;
    [ObservableProperty]
    LoanStatus? selectedLoanStatus = LoanStatus.Unpaid;
    [ObservableProperty]
    LoanStatus loanStatus;
    [ObservableProperty]
    StudentLoanType? selectedStudentLoanType = StudentLoanType.Other;
    [ObservableProperty]
    StudentLoanType studentLoanTypes;
    [ObservableProperty]
    FedLoanType? selectedFedLoanType = FedLoanType.DirectSub;
    [ObservableProperty]
    FedLoanType fedLoanType;
    [ObservableProperty]
    InterestType? selectedInterestType = InterestType.Fixed;
    [ObservableProperty]
    InterestType interestType;
    [ObservableProperty]
    string? schoolName;
    [ObservableProperty]
    string? currentOwner;
    [ObservableProperty]
    string guarantor;
    [ObservableProperty]
    DateTime disbursementDate;
    [ObservableProperty]
    LoanRepaymentPlans? selectedRepaymentPlan = LoanRepaymentPlans.Standard;
    [ObservableProperty]
    LoanRepaymentPlans repaymentPlan;


    /// <summary>
    /// Displays the Add frame.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [RelayCommand]
    async Task AddLoan(bool e)
    {
        try
        {
            if (IsAddLoanVisible)
            {
                IsAddLoanVisible = false;
                AddOrClose = "Add Loan";
            }
            else if (!IsAddLoanVisible && !Edit)
            {
                IsAddLoanVisible = true;
                AddOrClose = "Cancel";
                await ClearAddLoan();
            }
            else
            {
                IsAddLoanVisible = true;
                AddOrClose = "Cancel";
                EditLoan(new LoanModel());
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unknown Error", ex.Message, "OK");
        }
    }

    [RelayCommand]
    public async Task SaveLoan()
    {
        try
        {
            LoanModel newLoan = new LoanModel
            {
                Name = LoanName ?? "Default",
                Amount = LoanAmount,
                MonthlyPayment = Convert.ToDouble(MonthlyPayment),
                InterestRate = InterestRate,
                TotalInterest = TotalInterest,
                RemainingBalance = Convert.ToDouble(RemainingBalance),
                LoanType = SelectedLoanType.ToString(),
                Servicer = Servicer,
                StudentLoanType = SelectedStudentLoanType.ToString(),
                DueDate = DueDate,
                PaymentDate = PaymentDate,
                MonthlyPaid = IsPaid,
                LoanStatus = SelectedLoanStatus.ToString(),
                AutoPayId = AutoPayId == 0 ? null : AutoPayId,
                Transactions = Transactions,
                UserId = Constants.CurrentUser.Id,
                RemainingInterest = 0,
                FedLoanType = SelectedFedLoanType.ToString(),
                InterestType = SelectedInterestType.ToString(),
                SchoolName = SchoolName,
                CurrentOwner = CurrentOwner,
                Guarantor = Guarantor,
                DisbursementDate = DisbursementDate,
                RepaymentPlan = SelectedRepaymentPlan.ToString(),
            };

            if (newLoan.UserId == 0) { await Shell.Current.DisplayAlert("Error", "Unable to connect this loan to your account. Please try again later.", "OK"); return; }

            //check if this loan exists in database
            if (_db.loanModels.Any(l => l.Id == newLoan.Id))
            {
                _db.loanModels.Update(newLoan);
                Loans[Loans.IndexOf(SelectedLoan)] = newLoan;
                Constants.ConstLoans[Constants.ConstLoans.IndexOf(SelectedLoan)] = newLoan;
            }
            else
            {
                await _db.loanModels.AddAsync(newLoan);
                Loans.Add(newLoan);
                Constants.ConstLoans.Add(newLoan);
            }

            LoanString = Loans?.Count() > 0 ? $"You have {Loans.Count()} loans with a total of ${Loans.Sum(l => l.Amount)}" : "You have no active loans!";
            await _db.SaveChangesAsync();
            SelectedLoan = newLoan;
            IsAddLoanVisible = false;
            AddOrClose = "Add Loan";

            await ClearAddLoan();
            await Shell.Current.GoToAsync("//.");
            return;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.GetType() == typeof(SqliteException))
            {
                SqliteException sqlEx = (SqliteException)ex.InnerException;
                switch (sqlEx.SqliteErrorCode)
                {
                    case 19: // Foreign key violation
                        await Shell.Current.DisplayAlert("Database Error", $"There was a problem with one of the foreign keys.{Environment.NewLine}{ex.Message}", "OK");
                        break;
                    default:
                        await Shell.Current.DisplayAlert("Database Error", $"An unknown database error occurred.{Environment.NewLine}{ex.Message}", "OK");
                        break;
                }
                return;
            }
            else
            {
                await Shell.Current.DisplayAlert("Database Error", $"A database error occurred.{Environment.NewLine}{ex.Message}", "OK");
                return;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            return;
        }
    }

    //[RelayCommand]
    //public void EditLoan()
    //{
    //    LoanModel? loan = _db.loanModels.Find(SelectedLoan.Id);

    //    if (loan != null)
    //    {
    //        LoanName = loan.Name;
    //        LoanAmount = loan.Amount;
    //        InterestRate = loan.InterestRate;
    //        TotalInterest = loan.TotalInterest;
    //        MonthlyPayment = loan.MonthlyPayment.ToString();
    //        RemainingBalance = loan.RemainingBalance.ToString();
    //        DueDate = loan.DueDate;
    //        PaymentDate = loan.PaymentDate;
    //        PaidOff = loan.MonthlyPaid;
    //        SelectedLoanType = Enum.Parse<LoanType>(loan.LoanType);
    //        SelectedStudentLoanType = Enum.Parse<StudentLoanType>(loan.StudentLoanType);
    //        IsStudentLoan = Enum.Parse<LoanType>(loan.LoanType) == LoanType.Student;
    //        SelectedLoanStatus = Enum.Parse<LoanStatus>(loan.LoanStatus);
    //        SelectedFedLoanType = Enum.Parse<FedLoanType>(loan.FedLoanType);
    //        SelectedInterestType = Enum.Parse<InterestType>(loan.InterestType);
    //        SchoolName = loan.SchoolName;
    //        CurrentOwner = loan.CurrentOwner;
    //        Guarantor = loan.Guarantor;
    //        DisbursementDate = loan.DisbursementDate;
    //        SelectedRepaymentPlan = Enum.Parse<LoanRepaymentPlans>(loan.RepaymentPlan);
    //    }

    //    return;
    //}

    [RelayCommand]
    public async Task EditLoan(LoanModel loan)
    {
        try
        {
            //check if the navigation stack is empty
            if (Shell.Current.Navigation.NavigationStack.Count > 0)
                new LoanViewModel(_serviceProvider);

            LoanString = Constants.ConstLoans.Count() > 0 ? $"You have {Constants.ConstLoans.Count()} loans with a total of ${Constants.ConstLoans.Sum(l => l.Amount)}" : "You have no active loans!";


            LoanModel? Loan = _db.loanModels.Where(l => l.Id == loan.Id).First() ?? new LoanModel();

            if (Loan != null && _db.loanModels.Any(l => l == Loan))
            {
                await AddLoan(true);

                LoanName = Loan.Name;
                LoanAmount = Loan.Amount;
                InterestRate = Loan.InterestRate;
                TotalInterest = Loan.TotalInterest;
                MonthlyPayment = Loan.MonthlyPayment.ToString();
                RemainingBalance = Loan.RemainingBalance.ToString();
                DueDate = Loan.DueDate;
                PaymentDate = Loan.PaymentDate;
                PaidOff = Loan.MonthlyPaid;
                SelectedLoanType = Enum.Parse<LoanType>(Loan.LoanType);
                SelectedStudentLoanType = Enum.Parse<StudentLoanType>(Loan.StudentLoanType);
                IsStudentLoan = Enum.Parse<LoanType>(Loan.LoanType) == LoanType.Student;
                SelectedLoanStatus = Enum.Parse<LoanStatus>(Loan.LoanStatus);
                SelectedFedLoanType = Enum.Parse<FedLoanType>(Loan.FedLoanType);
                SelectedInterestType = Enum.Parse<InterestType>(Loan.InterestType);
                SchoolName = Loan.SchoolName;
                CurrentOwner = Loan.CurrentOwner;
                Guarantor = Loan.Guarantor;
                DisbursementDate = Loan.DisbursementDate;
                SelectedRepaymentPlan = Enum.Parse<LoanRepaymentPlans>(Loan.RepaymentPlan);
            }
        }
        catch
        {
            throw;
        }

        
        await Task.CompletedTask;
        return;
    }

    [RelayCommand]
    public void LoanTypeSelected()
    {
        IsStudentLoan = SelectedLoanType == LoanType.Student;
    }

    [RelayCommand]
    public void RefreshLoansCommand()
    {
        Loans = new ObservableCollection<LoanModel>(Constants.ConstLoans);
        LoanString = Constants.ConstLoans.Count() > 0 ? $"You have {Constants.ConstLoans.Count()} loans with a total of ${Constants.ConstLoans.Sum(l => l.Amount)}" : "You have no active loans!";
        return;
    }
    #endregion

    #region Methods
    private async Task ClearAddLoan()
    {
        LoanName = string.Empty;
        LoanAmount = 0;
        InterestRate = 0;
        TotalInterest = 0;
        MonthlyPayment = string.Empty;
        RemainingBalance = string.Empty;
        DueDate = DateTime.Now.AddDays(7);
        PaymentDate = DateTime.Now.AddDays(6);
        PaidOff = false;
        SelectedLoanType = LoanType.Student;
        SelectedStudentLoanType = StudentLoanType.Federal;
        SelectedLoanStatus = LoanStatus.Unpaid;
        SelectedFedLoanType = FedLoanType.DirectUnsub;
        SelectedInterestType = InterestType.Fixed;
        SchoolName = string.Empty;
        CurrentOwner = string.Empty;
        Guarantor = string.Empty;
        DisbursementDate = DateTime.Now;
        SelectedRepaymentPlan = LoanRepaymentPlans.Standard;

        await Task.Yield();
        return;
    }
    #endregion

    private void Constants_ConstLoansChanged(object sender, EventArgs e)
    {
        // Update Jobs with the new contents of ConstJobs
        Loans.Clear();
        foreach (var loan in Constants.ConstLoans)
        {
            Loans.Add(loan);
        }
    }

    // Make sure to unsubscribe from the event when the ViewModel is destroyed
    // to prevent memory leaks.
    ~LoanViewModel()
    {
        Constants.ConstLoansChanged -= Constants_ConstLoansChanged;
    }
}

public enum LoanType
{
    Auto,
    Personal,
    Student,
    Home,
    Business,
    Other
}

public enum LoanStatus
{
    Paid,
    Unpaid,
    Defaulted,
    Forgiven,
    Forebearance
}

public enum StudentLoanType
{
    Federal,
    Private,
    NA,
    Other
}

public enum FedLoanType
{
    DirectSub,
    DirectUnsub,
    DirectPlus,
    DirectConsolidation,
    Perkins,
    FFELSub,
    FFELUnsub,
    FFELPlus,
    FFELConsolidation,
    ParentPlus,
    GradPlus,
    NA,
    Other
}

public enum InterestType
{
    Simple,
    Compound,
    Amortized,
    Fixed,
    NA
}

public enum LoanRepaymentPlans
{
    Standard,
    Graduated,
    Extended,
    IncomeBased,
    PayAsYouEarn,
    RevisedPayAsYouEarn,
    IncomeContingent,
    IncomeSensitive,
    SAVE,
    NA
}