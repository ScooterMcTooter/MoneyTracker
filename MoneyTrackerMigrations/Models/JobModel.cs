using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class JobModel
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Company { get; set; }
    public string? Description { get; set; }
    public int LocationId { get; set; }
    public LocationModel? Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } = null;
    public int Hours { get; set; }
    public bool? IsActive { get; set; } = true;
    public double HourlyWage { get; set; }
    public double YearlyWage { get; set; }
    public bool IsSalary { get; set; } = true;
    public int PayFrequencyInWeeks { get; set; } = 2;
    public bool DirectDeposit { get; set; } = true;
    public string? Status { get; set; }
    public string? WorkLocation { get; set; }
    public int AccountId { get; set; }
    public AccountModel? Account { get; set; }
    public DateTime FirstPayDate { get; set; } = DateTime.Now;
    public UserModel? User { get; set; }
}
