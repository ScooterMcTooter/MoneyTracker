using MoneyTrackerMigrations.Models;

namespace MoneyTracker;

public static class Constants
{
    public static List<TransactionTypeViewModel> TransactionTypes = new List<TransactionTypeViewModel>
    {
        new TransactionTypeViewModel { Id = 1, Type = "Cash" },
        new TransactionTypeViewModel { Id = 2, Type = "Debit" },
        new TransactionTypeViewModel { Id = 3, Type = "Credit" },
        new TransactionTypeViewModel { Id = 4, Type = "Credit Card" },
        new TransactionTypeViewModel { Id = 5, Type = "Apple Pay" },
        new TransactionTypeViewModel { Id = 6, Type = "Venmo" },
        new TransactionTypeViewModel { Id = 7, Type = "PayPal" },
        new TransactionTypeViewModel { Id = 8, Type = "ACH Recurring" },
        new TransactionTypeViewModel { Id = 9, Type = "ACH Once" },
        new TransactionTypeViewModel { Id = 10, Type = "Check" },
        new TransactionTypeViewModel { Id = 11, Type = "Cash App" },
        new TransactionTypeViewModel { Id = 12, Type = "Internal Transfer" },
        new TransactionTypeViewModel { Id = 13, Type = "External Transfer" },
        new TransactionTypeViewModel { Id = 14, Type = "Deposit" },
        new TransactionTypeViewModel { Id = 15, Type = "ATM Withdrawal" },
        new TransactionTypeViewModel { Id = 16, Type = "ATM Deposit" },
        new TransactionTypeViewModel { Id = 17, Type = "Mobile Deposit" },
        new TransactionTypeViewModel { Id = 18, Type = "Other" },
    };

    public static bool IsAuthenticated = false;

    public const string PasswordReq = "Password must be between 15 and 20 characters and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
    public const string PasswordMissmatch = "Passwords did not match!";
    public const string UsernameReq = "Username must be between 5 and 20 characters with no numbers or spaces.";

    public static UserModel CurrentUser = new UserModel();

    public static DateTime MaxDate = DateTime.Now.AddYears(-18).Date;
}