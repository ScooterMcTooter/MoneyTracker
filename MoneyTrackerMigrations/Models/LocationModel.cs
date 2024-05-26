using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MoneyTrackerMigrations.Models;

public class LocationModel
{
    [Key]
    public int Id { get; set; }
    public string? Address { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    [StringLength(2)]
    public string? State { get; set; }
    public int? Zip { get; set; }
    public virtual JobModel? Job { get; set; }
}
