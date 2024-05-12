using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Models;
using MoneyTracker.ViewModels;

public class ApplicationDbContext : DbContext
{
    public DbSet<AccountModel> accountModels { get; set; }
    public DbSet<AutoPayModel> autoPayModels { get; set; }
    public DbSet<LoanModel> loanModels { get; set; }
    public DbSet<BucketModel> bucketModels { get; set; }
    public DbSet<TransactionModel> transactionModels { get; set; }
    public DbSet<UserModel> userModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        string dbPath = "MoneyTracker_dev.db";
#else
        string dbPath = "MoneyTracker.db";
#endif

        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string myAppDataPath = Path.Combine(appDataPath, "MoneyTracker");

        // Create the directory if it doesn't exist
        if (!Directory.Exists(myAppDataPath))
        {
            Directory.CreateDirectory(myAppDataPath);
        }

        // Specify the path to the database file
        dbPath = Path.Combine(myAppDataPath, dbPath);

        // Create the SQLite database if it doesn't exist
        if (!File.Exists(dbPath))
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                optionsBuilder.UseSqlite(connection.ConnectionString);
            }
        }
        else
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Tables and PrimaryKeys
        modelBuilder.Entity<AccountModel>().ToTable("FinanceAccounts").HasKey(i => i.Id);
        modelBuilder.Entity<AutoPayModel>().ToTable("AutoPays").HasKey(i => i.Id);
        modelBuilder.Entity<LoanModel>().ToTable("Loans").HasKey(i => i.Id);
        modelBuilder.Entity<BucketModel>().ToTable("SavingsBuckets").HasKey(i => i.Id);
        modelBuilder.Entity<TransactionModel>().ToTable("Transactions").HasKey(i => i.Id);
        modelBuilder.Entity<UserModel>().ToTable("Users").HasKey(i => i.Id);
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
        #endregion
    }
}
