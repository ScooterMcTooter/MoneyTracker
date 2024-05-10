using Microsoft.EntityFrameworkCore;
using MoneyTracker.ViewModels;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountViewModel> accountViewModels { get; set; }
    public DbSet<AutoPayViewModel> autoPayViewModels { get; set; }
    public DbSet<LoansViewModel> loansViewModels { get; set; }
    public DbSet<SavingsBucketsViewModel> savingsBucketsViewModels { get; set; }
    public DbSet<TransactionsViewModel> transactionsViewModels { get; set; }
    public DbSet<TransactionTypeViewModel> transactionTypeViewModels { get; set; }
    public DbSet<UserViewModel> userViewModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        string dbName = "MoneyTracker_dev.db";
#else
        string dbName = "MoneyTracker.db";
#endif
        optionsBuilder.UseSqlite($"DataSource={dbName}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Tables and PrimaryKeys
        modelBuilder.Entity<AccountViewModel>().ToTable("FinanceAccounts").HasKey(i => i.Id);
        modelBuilder.Entity<AutoPayViewModel>().ToTable("AutoPays").HasKey(i => i.Id);
        modelBuilder.Entity<LoansViewModel>().ToTable("Loans").HasKey(i => i.Id);
        modelBuilder.Entity<SavingsBucketsViewModel>().ToTable("SavingsBuckets").HasKey(i => i.Id);
        modelBuilder.Entity<TransactionsViewModel>().ToTable("Transactions").HasKey(i => i.Id);
        modelBuilder.Entity<TransactionTypeViewModel>().ToTable("TransactionTypes").HasKey(i => i.Id);
        modelBuilder.Entity<UserViewModel>().ToTable("Users").HasKey(i => i.Id);
        #endregion

        #region Relationships
        // UserViewModel to AccountViewModel
        modelBuilder.Entity<AccountViewModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(l => l.Id);

        // UserViewModel to LoansViewModel
        modelBuilder.Entity<LoansViewModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId);
        
        //AutoPayViewModel to LoansViewModel
        modelBuilder.Entity<LoansViewModel>()
            .HasOne(l => l.autoPay)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.AutoPayId);

        // UserViewModel to AutoPayViewModel
        modelBuilder.Entity<AutoPayViewModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.AutoPays)
            .HasForeignKey(l => l.UserId);

        // UserViewModel to SavingsBucketsViewModel
        modelBuilder.Entity<SavingsBucketsViewModel>()
            .HasMany(l => l.Users)
            .WithMany(u => u.SavingsBuckets)
            .UsingEntity(j => j.ToTable("UserSavingsBuckets"));

        // UserViewModel to TransactionsViewModel
        modelBuilder.Entity<TransactionTypeViewModel>()
            .HasOne(l => l.AutoPay)
            .WithMany(u => u.TransactionType)
            .HasForeignKey(l => l.Id);

        
        #endregion
    }
}
