using System.Security;

namespace MoneyTrackerMigrations
{

    public static class Constants
    {
        public static bool IsAuthenticated = false;

        public const string PasswordReq = "Password must be between 15 and 20 characters and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
        public const string PasswordMissmatch = "Passwords did not match!";
        public const string UsernameReq = "Username must be between 5 and 20 characters with no numbers or spaces.";

        public static DateTime MaxDate = DateTime.Now.AddYears(-18).Date;

        public static SecureString? ConnectionString;

        public static SecureString SetConnectionString()
        {
            ConnectionString = new SecureString();
#if DEBUG
            foreach (char c in "Data Source=MoneyTracker_dev.db")
            {
                ConnectionString.AppendChar(c);
            }
            ConnectionString.MakeReadOnly();
#elif RELEASE
            foreach (char c in "Data Source=MoneyTracker.db")
            {
                ConnectionString.AppendChar(c);
            }
#endif
            return ConnectionString;
        }
    }
}