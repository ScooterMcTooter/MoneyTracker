using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MoneyTrackerMigrations;
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
}
