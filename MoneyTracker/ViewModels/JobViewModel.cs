using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyTrackerMigrations;
using MoneyTrackerMigrations.Models;

namespace MoneyTracker.ViewModels;

public partial class JobViewModel : ObservableObject
{
    private readonly ApplicationDbContext _db;

    public JobViewModel(ApplicationDbContext db)
    {
        _db = db;
        JobStartDate = DateTime.Now;
        JobEndDate = null;
        JobType = JobType.None;
        JobLocation = JobLocation.None;
        JobStatus = JobStatus.None;
        JobHours = JobHours.None;
    }

    private bool _jobIsCurrent;
    public bool JobIsCurrent
    {
        get { return _jobIsCurrent; }
        set
        {
            if (_jobIsCurrent != value)
            {
                _jobIsCurrent = value;
                OnPropertyChanged(nameof(JobIsCurrent));
            }
        }
    }

    #region Observable Properties
    [ObservableProperty]
    string jobCompany = string.Empty;
    [ObservableProperty]
    string jobTitle = string.Empty;
    [ObservableProperty]
    string? address;
    [ObservableProperty]
    string? address2;
    [ObservableProperty]
    string? city;
    [ObservableProperty]
    string? state;
    [ObservableProperty]
    int? zip;
    [ObservableProperty]
    string jobDescription = string.Empty;
    [ObservableProperty]
    double jobPayYearly;
    [ObservableProperty]
    double jobPayHourly;
    [ObservableProperty]
    int jobPayFrequencyInWeeks = 2;
    [ObservableProperty]
    DateTime jobStartDate;
    [ObservableProperty]
    DateTime? jobFirstPay = null;
    [ObservableProperty]
    DateTime? jobEndDate = null;
    [ObservableProperty]
    JobType jobType;
    [ObservableProperty]
    JobLocation jobLocation;
    [ObservableProperty]
    JobStatus jobStatus;
    [ObservableProperty]
    JobHours jobHours;
    #endregion
    #region Enum Lists
    public List<JobType> JobTypesValues => Enum.GetValues(typeof(JobType)).Cast<JobType>().ToList();
    public List<JobLocation> JobLocationValues => Enum.GetValues(typeof(JobLocation)).Cast<JobLocation>().ToList();
    public List<JobStatus> JobStatusValues => Enum.GetValues(typeof(JobStatus)).Cast<JobStatus>().ToList();
    public List<JobHours> JobHoursValues => Enum.GetValues(typeof(JobHours)).Cast<JobHours>().ToList();
    public List<State> StateValues => Enum.GetValues(typeof(State)).Cast<State>().ToList();
    // ...

    [RelayCommand]
    public async Task SaveJob()
    {
        // Save the job to the database with the current user ID
        JobModel job = new JobModel()
        {
            Company = JobCompany,
            Title = JobTitle,
            Location = new LocationModel()
            {
                Address = Address,
                Address2 = Address2,
                City = City,
                State = State,
                Zip = Zip
            },
            Description = JobDescription,
            HourlyWage = JobPayHourly == 0 ? JobPayYearly / JobHoursConversions(JobHours.ToString()) : JobPayHourly,
            YearlyWage = JobPayYearly == 0 ? JobPayHourly * JobHoursConversions(JobHours.ToString()) : JobPayYearly,
            StartDate = JobStartDate,
            EndDate = JobEndDate,
            IsSalary = JobType == JobType.Salary ? true : false,
            WorkLocation = JobLocation.ToString(),
            Status = JobStatus.ToString(),
            Hours = JobHoursConversions(JobHours.ToString())
        };

        await _db.jobModels.AddAsync(job);
        await _db.SaveChangesAsync();

        //Take the user back to their last page they were on
        await Shell.Current.GoToAsync("//..");
    }

    [RelayCommand]
    public async Task ClearForm()
    {
        JobCompany = string.Empty;
        JobTitle = string.Empty;
        Address = string.Empty;
        Address2 = string.Empty;
        City = string.Empty;
        State = string.Empty;
        Zip = null;
        JobDescription = string.Empty;
        JobPayYearly = 0;
        JobPayHourly = 0;
        JobPayFrequencyInWeeks = 2;
        JobStartDate = DateTime.Now;
        JobEndDate = null;
        JobType = JobType.None;
        JobLocation = JobLocation.None;
        JobStatus = JobStatus.None;
        JobHours = JobHours.None;
        await Task.CompletedTask;
        return;
    }
    #endregion

    public int JobHoursConversions(string hours)
    {
       switch (hours)
        {
            case "40":
                return 40;
            case "36":
                return 36;
            case "32":
                return 32;
            case "28":
                return 28;
            case "24":
                return 24;
            case "20":
                return 20;
            case "16":
                return 16;
            case "12":
                return 12;
            case "8":
                return 8;
            case "4":
                return 4;
            default:
                return 0;
        }   
    }
}

#region enums
public enum JobType
{
    None,
    Hourly,
    Salary,
    Contract
}

public enum JobLocation
{
    None,
    Hybird,
    Remote,
    OnSite
}

public enum JobStatus
{
    None,
    Full_Time,
    Part_Time,
    Temporary,
    Contract,
    Contract_no_tax_taken,
    Seasonal
}

public enum JobHours
{
    None,
    Forty,
    Thirty_Six,
    Thirty_Two,
    Twenty_Eight,
    Twenty_Four,
    Twenty,
    Sixteen,
    Twelve,
    Eight,
    Four
}

public enum State
{
    AL,
    AK,
    AZ,
    AR,
    CA,
    CO,
    CT,
    DE,
    FL,
    GA,
    HI,
    ID,
    IL,
    IN,
    IA,
    KS,
    KY,
    LA,
    ME,
    MD,
    MA,
    MI,
    MN,
    MS,
    MO,
    MT,
    NE,
    NV,
    NH,
    NJ,
    NM,
    NY,
    NC,
    ND,
    OH,
    OK,
    OR,
    PA,
    RI,
    SC,
    SD,
    TN,
    TX,
    UT,
    VT,
    VA,
    WA,
    WV,
    WI,
    WY
}
#endregion