using CommunityToolkit.Mvvm.ComponentModel;

namespace MoneyTracker.ViewModels;

public partial class JobViewModel : ObservableObject
{
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
    double jobPay;
    [ObservableProperty]
    DateTime jobStartDate;
    [ObservableProperty]
    DateTime? jobEndDate;
    [ObservableProperty]
    JobType jobType;
    [ObservableProperty]
    JobLocation jobLocation;
    [ObservableProperty]
    JobStatus jobStatus;
    [ObservableProperty]
    JobHours jobHours;

    public List<JobType> JobTypesValues => Enum.GetValues(typeof(JobType)).Cast<JobType>().ToList();
    public List<JobLocation> JobLocationValues => Enum.GetValues(typeof(JobLocation)).Cast<JobLocation>().ToList();
    public List<JobStatus> JobStatusValues => Enum.GetValues(typeof(JobStatus)).Cast<JobStatus>().ToList();
    public List<JobHours> JobHoursValues => Enum.GetValues(typeof(JobHours)).Cast<JobHours>().ToList();
}

#region enums
public enum JobType
{
    Hourly,
    Salary,
    Contract
}

public enum JobLocation
{
    Hybird,
    Remote,
    OnSite
}

public enum JobStatus
{
    Full_Time,
    Part_Time,
    Temporary,
    Contract,
    Contract_no_tax_taken,
    Seasonal
}

public enum JobHours
{
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
#endregion