using Microsoft.EntityFrameworkCore;
using MoneyTracker.ViewModels;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public DbSet<UserViewModel> userViewModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        string dbName = "MoneyTracker_dev.db";
#else
        string dbName = "MoneyTracker.db";
#endif
        optionsBuilder.UseSqlite($"Filename={dbName}");
    }
}
