using Microsoft.EntityFrameworkCore;
using MoneyTrackerMigrations.Models;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.VisualBasic;

namespace MoneyTrackerMigrations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AccountModel> accountModels { get; set; }
    public DbSet<AutoPayModel> autoPayModels { get; set; }
    public DbSet<LoanModel> loanModels { get; set; }
    public DbSet<BucketModel> bucketModels { get; set; }
    public DbSet<TransactionModel> transactionModels { get; set; }
    public DbSet<TransactionTypeModel> transactionTypeModels { get; set; }
    public DbSet<UserModel> userModels { get; set; }
    public DbSet<SettingsModel> settingsModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MoneyTracker_dev.db");
#else
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MoneyTracker.db");
#endif

        optionsBuilder.UseSqlite($"Filename={dbPath}");
        //optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Tables and PrimaryKeys
        modelBuilder.Entity<AccountModel>().ToTable("FinanceAccounts").HasKey(i => i.Id);
        modelBuilder.Entity<AutoPayModel>().ToTable("AutoPays").HasKey(i => i.Id);
        modelBuilder.Entity<LoanModel>().ToTable("Loans").HasKey(i => i.Id);
        modelBuilder.Entity<BucketModel>().ToTable("SavingsBuckets").HasKey(i => i.Id);
        modelBuilder.Entity<TransactionModel>().ToTable("Transactions").HasKey(i => i.Id);
        modelBuilder.Entity<TransactionTypeModel>().ToTable("TransactionTypes").HasKey(i => i.Id);
        modelBuilder.Entity<UserModel>().ToTable("Users").HasKey(i => i.Id);
        modelBuilder.Entity<SettingsModel>().ToTable("Settings").HasKey(i => i.Id);
        #endregion

        #region Relationships
        //AccountModel to UserModel
        modelBuilder.Entity<AccountModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(l => l.UserId);

        //AccountModel to BucketModel
        modelBuilder.Entity<BucketModel>()
            .HasOne(l => l.Account)
            .WithMany(u => u.Buckets)
            .HasForeignKey(l => l.AccountId);

        //AccountModel to TransactionModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(l => l.Account)
            .WithMany(u => u.Transactions)
            .HasForeignKey(l => l.AccountId);

        //AccountModel to AutoPayModel
        modelBuilder.Entity<AutoPayModel>()
            .HasOne(l => l.Accounts)
            .WithMany(u => u.AutoPays)
            .HasForeignKey(l => l.AccountId);

        // UserModel to AccountModel
        modelBuilder.Entity<AccountModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Accounts)
            .HasForeignKey(l => l.UserId);

        // UserModel to LoanModel
        modelBuilder.Entity<LoanModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId);

        //AutoPayModel to UserModel
        modelBuilder.Entity<AutoPayModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.AutoPay)
            .HasForeignKey(l => l.UserId);

        //AutoPayModel to AccountModel
        modelBuilder.Entity<AutoPayModel>()
            .HasOne(l => l.Accounts)
            .WithMany(u => u.AutoPays)
            .HasForeignKey(l => l.AccountId);

        //AutopayModel to TransactionModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(l => l.AutoPay)
            .WithMany(u => u.Transactions)
            .HasForeignKey(l => l.AutoPayId);

        // UserModel to BucketModel
        modelBuilder.Entity<BucketModel>()
            .HasMany(l => l.Users)
            .WithMany(u => u.Buckets)
            .UsingEntity(j => j.ToTable("UserBuckets"));

        //TransactionModel to BucketModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(l => l.Bucket)
            .WithMany(u => u.Transactions)
            .HasForeignKey(l => l.BucketId);

        //BucketModel to AccountModel
        modelBuilder.Entity<BucketModel>()
            .HasOne(l => l.Account)
            .WithMany(u => u.Buckets)
            .HasForeignKey(l => l.AccountId);

        //TransactionModel to AutoPayModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(l => l.AutoPay)
            .WithMany(u => u.Transactions)
            .HasForeignKey(l => l.AutoPayId);

        //TransactionModel to UserModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(l => l.UserId);

        //LoanModel to UserModel
        modelBuilder.Entity<LoanModel>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId);

        //LoanModel to TransactionModel
        modelBuilder.Entity<TransactionModel>()
            .HasOne(l => l.Loan)
            .WithMany(u => u.Transactions)
            .HasForeignKey(l => l.LoanId);

        //LoanModel to AutoPayModel
        modelBuilder.Entity<AutoPayModel>()
            .HasOne(l => l.Loan)
            .WithMany(u => u.AutoPay)
            .HasForeignKey(l => l.LoanId);

        //SettingsModel to UserModel one to one
        modelBuilder.Entity<SettingsModel>()
            .HasOne(l => l.User)
            .WithOne(u => u.Settings)
            .HasForeignKey<SettingsModel>(l => l.UserId);

        #endregion

        #region SeedData
        modelBuilder.Entity<TransactionTypeModel>().HasData(
                       new TransactionTypeModel
                       {
                           Id = 1,
                           Type = "Cash"
                       },
                       new TransactionTypeModel
                       {
                           Id = 2,
                           Type = "Debit"
                       },
                       new TransactionTypeModel
                       {
                           Id = 3,
                           Type = "Cash"
                       },
                       new TransactionTypeModel
                       {
                           Id = 4,
                           Type = "Credit (Debit Card)"
                       },
                       new TransactionTypeModel
                       {
                           Id = 5,
                           Type = "Credit (Credit Card)"
                       },
                       new TransactionTypeModel
                       {
                           Id = 6,
                           Type = "ApplePay"
                       }, new TransactionTypeModel
                       {
                           Id = 7,
                           Type = "Venmo"
                       }, new TransactionTypeModel
                       {
                           Id = 8,
                           Type = "PayPal"
                       }, new TransactionTypeModel
                       {
                           Id = 9,
                           Type = "CashApp"
                       }, new TransactionTypeModel
                       {
                           Id = 10,
                           Type = "ACHRecurring"
                       }, new TransactionTypeModel
                       {
                           Id = 11,
                           Type = "ACHOnce"
                       }, new TransactionTypeModel
                       {
                           Id = 12,
                           Type = "Check"
                       }, new TransactionTypeModel
                       {
                           Id = 13,
                           Type = "InternalTransfer"
                       }, new TransactionTypeModel
                       {
                           Id = 14,
                           Type = "ExternalTransfer"
                       }, new TransactionTypeModel
                       {
                           Id = 15,
                           Type = "ATM Withdrawal"
                       }, new TransactionTypeModel
                       {
                           Id = 16,
                           Type = "ATM Deposit"
                       }, new TransactionTypeModel
                       {
                           Id = 17,
                           Type = "Mobile Deposit"
                       }, new TransactionTypeModel
                       {
                           Id = 18,
                           Type = "Deposit"
                       }, new TransactionTypeModel
                       {
                           Id = 19,
                           Type = "Other"
                       });
        #endregion
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite(Constants.SetConnectionString().ToString());

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}

