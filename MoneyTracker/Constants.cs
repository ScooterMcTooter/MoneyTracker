namespace MoneyTracker.Models
{

    public static class Constants
    {
        public static List<TransactionTypeModel> TransactionTypes = new List<TransactionTypeModel>
    {
        new TransactionTypeModel { Id = 1, Type = "Cash" },
        new TransactionTypeModel { Id = 2, Type = "Debit" },
        new TransactionTypeModel { Id = 3, Type = "Credit" },
        new TransactionTypeModel { Id = 4, Type = "Credit Card" },
        new TransactionTypeModel { Id = 5, Type = "Apple Pay" },
        new TransactionTypeModel { Id = 6, Type = "Venmo" },
        new TransactionTypeModel { Id = 7, Type = "PayPal" },
        new TransactionTypeModel { Id = 8, Type = "ACH Recurring" },
        new TransactionTypeModel { Id = 9, Type = "ACH Once" },
        new TransactionTypeModel { Id = 10, Type = "Check" },
        new TransactionTypeModel { Id = 11, Type = "Cash App" },
        new TransactionTypeModel { Id = 12, Type = "Internal Transfer" },
        new TransactionTypeModel { Id = 13, Type = "External Transfer" },
        new TransactionTypeModel { Id = 14, Type = "Deposit" },
        new TransactionTypeModel { Id = 15, Type = "ATM Withdrawal" },
        new TransactionTypeModel { Id = 16, Type = "ATM Deposit" },
        new TransactionTypeModel { Id = 17, Type = "Mobile Deposit" },
        new TransactionTypeModel { Id = 18, Type = "Other" },
    };
    }
}