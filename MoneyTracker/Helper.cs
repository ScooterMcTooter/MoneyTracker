using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MoneyTracker;
public class Helper
{
    /// <summary>
    /// Hashes the given password using SHA256 encryption.
    /// </summary>
    /// <param name="password">The password to hash.</param>
    /// <returns>The hashed password as a Base64 string.</returns>
    public string HashPassword(string password)
    {
        var sha256 = SHA256.Create();
        byte[] byteRepresentation = Encoding.UTF8.GetBytes(password);
        byte[] hashedBytes = sha256.ComputeHash(byteRepresentation);
        return Convert.ToBase64String(hashedBytes);
    }

    /// <summary>
    /// Hashes the given account number using SHA256 encryption.
    /// </summary>
    /// <param name="accountNumber">The account number to hash.</param>
    /// <returns>The hashed account number as a hexadecimal string.</returns>
    public string HashAccountNumber(string accountNumber)
    {
        if (accountNumber == null) throw new ArgumentNullException(nameof(accountNumber));
        else if (CheckDigitsAndNoWhitespaces(accountNumber)) throw new ArgumentException("The account number must contain only digits and no whitespaces.", nameof(accountNumber));

        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(accountNumber));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    /// <summary>
    /// Checks if the input string contains only digits and no whitespaces.
    /// </summary>
    /// <param name="input">The input string to check.</param>
    /// <returns>True if the input string contains only digits and no whitespaces, otherwise false.</returns>
    public bool CheckDigitsAndNoWhitespaces(string input)
    {
        Regex regex = new Regex(@"^\d+$");
        return regex.IsMatch(input);
    }

    /// <summary>
    /// Gets the color for the balance based on its value.
    /// </summary>
    /// <param name="balance">The balance value.</param>
    /// <returns>The color code as a string.</returns>
    public string GetBalanceColor(double balance, bool opposite = false)
    {
        string color = string.Empty;
        if (!opposite)
            color = balance > 0 ? "#20C11B" : "#FF2D00";
        else
            color = balance > 0 ? "#FF2D00" : "#20C11B";

        return color;
    }

    public double PayvsBills(double bills)
    {
        double sum = 0;
        var jobs = Constants.ConstJobs.Where(j => j.IsActive == true).ToList();

        foreach (var job in jobs)
        {
            DateTime nextPayDate = job.FirstPayDate;
            int payFrequencyInWeeks = job.PayFrequencyInWeeks;
            int numberOfPaychecks = GetNumberOfPaychecks(payFrequencyInWeeks, nextPayDate);
            sum += job.PayCheckAmount * numberOfPaychecks;
        }

        return Math.Round(sum - bills, 2);
    }
    /// <summary>
    /// Calculates the number of paychecks based on pay frequency and the next pay date.
    /// </summary>
    /// <param name="payFrequencyInWeeks">The pay frequency in weeks.</param>
    /// <param name="nextPayDate">The next pay date.</param>
    /// <returns>The number of paychecks.</returns>
    public int GetNumberOfPaychecks(int payFrequencyInWeeks, DateTime nextPayDate)
    {
        DateTime next = nextPayDate;
        DateTime CheckDate = DateTime.Now.Date;
        int numberOfPaychecks = 1;

        while (next.Month == CheckDate.Month)
        {
            if (next.AddDays((payFrequencyInWeeks * 7)).Month == CheckDate.Month)
            {
                numberOfPaychecks++;
            }
            next = next.AddDays((payFrequencyInWeeks * 7));
        }

        next = nextPayDate;

        if (nextPayDate.Month == CheckDate.Month)
        {
            //Find the amount of pays that happened in the last month bassed on the next pay date and pay frequency
            while (next.Month == DateTime.Now.Month)
            {
                if (next.AddDays(-1 * (payFrequencyInWeeks * 7)).Month == CheckDate.Month)
                {
                    numberOfPaychecks++;
                }
                next = next.AddDays(-1 * (payFrequencyInWeeks * 7));
            }
        }

        return numberOfPaychecks;
    }
}
