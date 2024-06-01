using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MoneyTrackerMigrations;
using MoneyTrackerMigrations.Models;
using System.Collections.ObjectModel;

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
        Accounts = new ObservableCollection<AccountModel>(_db.accountModels.Where(a => a.UserId == Constants.CurrentUser.Id).ToList());
        Locations = new ObservableCollection<LocationModel>(_db.locationModels.Where(l => l.UserId == Constants.CurrentUser.Id).ToList());
        Jobs = new ObservableCollection<JobModel>(_db.jobModels.Where(j => j.UserId == Constants.CurrentUser.Id).ToList());

        //Constants.CurrentUser.
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
    string addOrClose = "Add Job";
    [ObservableProperty]
    bool jobPage = true;
    [ObservableProperty]
    bool addJobVisible = false;
    [ObservableProperty]
    int? jobId = null;
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
    JobState? state;
    [ObservableProperty]
    int? zip;
    [ObservableProperty]
    string jobDescription = string.Empty;
    [ObservableProperty]
    double? jobPay = null;
    [ObservableProperty]
    double? jobPayTaxes = null;
    [ObservableProperty]
    double? jobPayYearly = null;
    [ObservableProperty]
    double jobPayHourly;
    [ObservableProperty]
    int jobPayFrequencyInWeeks = 2;
    [ObservableProperty]
    bool directDeposit = true;
    [ObservableProperty]
    ObservableCollection<JobModel>? jobs;
    [ObservableProperty]
    JobModel? selectedJob;
    [ObservableProperty]
    ObservableCollection<AccountModel>? accounts;
    [ObservableProperty]
    AccountModel? selectedAccount = null;
    [ObservableProperty]
    ObservableCollection<LocationModel>? locations;
    [ObservableProperty]
    LocationModel? selectedLocation = null;
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
    public List<JobState> StateValues => Enum.GetValues(typeof(JobState)).Cast<JobState>().ToList();
    // ...
    #endregion
    #region Commands
    [RelayCommand]
    public async Task SaveJob()
    {
        if (!AreFieldsValid())
        {
            await Shell.Current.DisplayAlert("Error", "Please fill out all fields", "OK");
            return;
        }

        int hours = JobHoursConversions(JobHours.ToString());
        double pay = JobPay ?? 1;
        double payTaxes = JobPayTaxes ?? 1;
        double payYearly = JobPayYearly == null ? Math.Round((52 / JobPayFrequencyInWeeks) * pay, 2) : (double)JobPayYearly;
        double wage = Math.Round(pay / hours, 2);

        LocationModel location = new LocationModel()
        {
            Address = Address,
            Address2 = Address2,
            City = City,
            State = State.ToString(),
            Zip = Zip
        };

        // Save the job to the database with the current user ID
        JobModel job = new JobModel()
        {
            Id = JobId ?? 0,
            Company = JobCompany,
            Title = JobTitle,
            Location = location,
            Description = JobDescription,
            PayCheckAmount = pay,
            PayCheckAmountBeforeTax = payTaxes,
            HourlyWage = wage,
            YearlyWage = payYearly,
            StartDate = JobStartDate,
            FirstPayDate = JobFirstPay ?? DateTime.Now,
            EndDate = JobEndDate,
            IsActive = JobIsCurrent,
            IsSalary = JobType == JobType.Salary ? true : false,
            PayFrequencyInWeeks = JobPayFrequencyInWeeks,
            DirectDeposit = DirectDeposit,
            WorkLocation = JobLocation.ToString(),
            Status = JobStatus.ToString(),
            Hours = hours,
            UserId = Constants.CurrentUser.Id,
            //User = Constants.CurrentUser,
            AccountId = SelectedAccount?.Id ?? null,
            //Account = SelectedAccount
        };

        try
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    if (JobId == null || JobId == 0)
                    {
                        //Check to see if the job is already in the database without comparing the ID
                        bool jobExists = JobCompare(job);

                        if (Jobs == null)
                        {
                            Jobs = new ObservableCollection<JobModel>() { job };
                            var entity = await _db.jobModels.AddAsync(job);
                            await _db.SaveChangesAsync();
                        }
                        else if (!jobExists)
                        {
                            Jobs.Add(job);
                            var entity = await _db.jobModels.AddAsync(job);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                            JobModel existingJob = await _db.jobModels.Where(j => j.UserId == Constants.CurrentUser.Id).FirstAsync();
                            bool isExistingJob = JobCompare(job, existingJob);

                            if (isExistingJob)
                            {
                                int index = Jobs.IndexOf(existingJob);
                                Jobs[index] = job;
                            }
                        }
                    }
                    else
                    {
                        _db.jobModels.Update(job);
                        await _db.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    await Shell.Current.DisplayAlert("Error", $"{ex}", "OK");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"{ex}", "OK");
            return;
        }

        //Take the user back to their last page they were on
        await ClearForm();
        AddJobVisible = false;
        AddOrClose = AddJobVisible ? "Close Add Job" : "Add Job";
        return;
    }

    [RelayCommand]
    public async Task ClearForm()
    {
        JobCompany = string.Empty;
        JobTitle = string.Empty;
        Address = string.Empty;
        Address2 = string.Empty;
        City = string.Empty;
        State = JobState.None;
        Zip = null;
        JobDescription = string.Empty;
        JobPay = null;
        JobPayYearly = 0;
        JobPayHourly = 0;
        JobPayTaxes = 0;
        JobPayFrequencyInWeeks = 2;
        JobStartDate = DateTime.Now;
        JobIsCurrent = false;
        JobEndDate = null;
        JobType = JobType.None;
        JobLocation = JobLocation.None;
        JobStatus = JobStatus.None;
        JobHours = JobHours.None;
        SelectedAccount = null;
        SelectedLocation = null;

        await Task.CompletedTask;
        return;
    }

    [RelayCommand]
    public async Task AddJob()
    {
        AddJobVisible = !AddJobVisible;
        AddOrClose = AddJobVisible ? "Close Add Job" : "Add Job";
        await Task.CompletedTask;
        return;
    }

    [RelayCommand]
    public async Task EditJob(JobModel job)
    {
        try
        {
            if (job == null)
            {
                await Shell.Current.DisplayAlert("Error", "Job not found when attempting to edit", "OK");
                return;
            }

            string hours = JobHoursConversions(job.Hours);

            //Set the form fields to the job that was selected
            JobId = job.Id;
            JobCompany = job.Company;
            JobTitle = job.Title;
            Address = job.Location?.Address;
            Address2 = job.Location?.Address2;
            City = job.Location?.City;
            State = Enum.Parse<JobState>(job.Location?.State);
            Zip = job.Location?.Zip;
            JobDescription = job.Description;
            JobPay = job.HourlyWage;
            JobPayYearly = job.YearlyWage;
            JobStartDate = job.StartDate;
            JobIsCurrent = job.IsActive ?? false;
            JobEndDate = job.EndDate;
            JobType = job.IsSalary ? JobType.Salary : JobType.Hourly;
            JobLocation = Enum.Parse<JobLocation>(job.WorkLocation);
            JobStatus = Enum.Parse<JobStatus>(job.Status);
            JobHours = Enum.Parse<JobHours>(hours);
            SelectedAccount = job.Account;

            AddJobVisible = true;
            //Accounts = new ObservableCollection<AccountModel>(_db.accountModels.Where(a => a.UserId == Constants.CurrentUser.Id).ToList() ?? new List<AccountModel>());
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"{ex}", "OK");
            return;
        }
        await Task.CompletedTask;
        return;
    }

    [RelayCommand]
    public async Task DeleteJob(JobModel job)
    {
        try
        {
            if (job == null)
            {
                await Shell.Current.DisplayAlert("Error", "Job not found when attempting to delete", "OK");
                return;
            }

            _db.Remove(job);
            await _db.SaveChangesAsync();

            Jobs.Remove(job);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error: failed to delete job", $"Try again in a bit.\r\n\r\nException:\r\n{ex}", "OK");
            return;
        }
        return;
    }
    #endregion

    #region Helper Methods
    public bool JobCompare(JobModel newJob, JobModel? dbJob = null)
    {
        if (dbJob == null) 
        {
            int hours = JobHoursConversions(JobHours.ToString());
            double pay = JobPay ?? 1;
            double payTaxes = JobPayTaxes ?? 1;
            double payYearly = JobPayYearly == null ? Math.Round((52 / JobPayFrequencyInWeeks) * pay, 2) : (double)JobPayYearly;
            double wage = Math.Round(pay / hours, 2);

            return _db.jobModels.Any(j =>
                            j.Company == JobCompany &&
                            j.Title == JobTitle &&
                            j.Location.Address == Address &&
                            j.Location.Address2 == Address2 &&
                            j.Location.City == City &&
                            j.Location.State == State.ToString() &&
                            j.Location.Zip == Zip &&
                            j.Description == JobDescription &&
                            j.PayCheckAmount == pay &&
                            j.PayCheckAmountBeforeTax == payTaxes &&
                            j.HourlyWage == wage &&
                            j.YearlyWage == payYearly &&
                            j.StartDate == JobStartDate &&
                            j.FirstPayDate == (JobFirstPay ?? DateTime.Now) &&
                            j.EndDate == JobEndDate &&
                            j.IsActive == JobIsCurrent &&
                            (j.IsSalary ? JobType.Salary : JobType.Hourly) == JobType &&
                            j.PayFrequencyInWeeks == JobPayFrequencyInWeeks &&
                            j.DirectDeposit == DirectDeposit &&
                            j.WorkLocation == JobLocation.ToString() &&
                            j.Status == JobStatus.ToString() &&
                            j.Hours == hours &&
                            j.UserId == Constants.CurrentUser.Id
                        );
        }
        else
        {
            if (dbJob.Company != newJob.Company)
                return false;
            if (dbJob.Title != newJob.Title)
                return false;
            if (dbJob?.Location?.Address != newJob?.Location?.Address)
                return false;
            if (dbJob?.Location?.Address2 != newJob?.Location?.Address2)
                return false;
            if (dbJob?.Location?.City != newJob?.Location?.City)
                return false;
            if (dbJob?.Location?.State != newJob?.Location?.State)
                return false;
            if (dbJob?.Location?.Zip != newJob?.Location?.Zip)
                return false;
            if (dbJob.Description != newJob.Description)
                return false;
            if (dbJob.PayCheckAmount != newJob.PayCheckAmount)
                return false;
            if (dbJob.PayCheckAmountBeforeTax != newJob.PayCheckAmountBeforeTax)
                return false;
            if (dbJob.HourlyWage != newJob.HourlyWage)
                return false;
            if (dbJob.YearlyWage != newJob.YearlyWage)
                return false;
            if (dbJob.StartDate != newJob.StartDate)
                return false;
            if (dbJob.FirstPayDate != newJob.FirstPayDate)
                return false;
            if (dbJob.EndDate != newJob.EndDate)
                return false;
            if (dbJob.IsActive != newJob.IsActive)
                return false;
            if (dbJob.IsSalary != newJob.IsSalary)
                return false;
            if (dbJob.PayFrequencyInWeeks != newJob.PayFrequencyInWeeks)
                return false;
            if (dbJob.DirectDeposit != newJob.DirectDeposit)
                return false;
            if (dbJob.WorkLocation != newJob.WorkLocation)
                return false;
            if (dbJob.Status != newJob.Status)
                return false;
            if (dbJob.Hours != newJob.Hours)
                return false;
            if (dbJob.UserId != newJob.UserId)
                return false;
            if (dbJob.AccountId != newJob.AccountId)
                return false;
        }
        return true;
    }

    public int JobHoursConversions(string hours)
    {
        switch (hours)
        {
            case "Forty":
                return 40;
            case "Thirty_Six":
                return 36;
            case "Thirty_Two":
                return 32;
            case "Twenty_Eight":
                return 28;
            case "Twenty_Four":
                return 24;
            case "Twenty":
                return 20;
            case "Sixteen":
                return 16;
            case "Twelve":
                return 12;
            case "Eight":
                return 8;
            case "Four":
                return 4;
            default:
                return 0;
        }
    }

    //Invert JobHoursConversions
    public string JobHoursConversions(int hours)
    {
        switch (hours)
        {
            case 40:
                return "Forty";
            case 36:
                return "Thirty_Six";
            case 32:
                return "Thirty_Two";
            case 28:
                return "Twenty_Eight";
            case 24:
                return "Twenty_Four";
            case 20:
                return "Twenty";
            case 16:
                return "Sixteen";
            case 12:
                return "Twelve";
            case 8:
                return "Eight";
            case 4:
                return "Four";
            default:
                return "None";
        }
    }

    public bool AreFieldsValid()
    {
        if (string.IsNullOrEmpty(JobCompany) || string.IsNullOrEmpty(JobTitle) || JobPay == 0 || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(City) || State == JobState.None || Zip == null || JobType == JobType.None || JobLocation == JobLocation.None || JobStatus == JobStatus.None || JobHours == JobHours.None)
        {
            return false;
        }
        return true;
    }
    #endregion
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

public enum JobState
{
    None,
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