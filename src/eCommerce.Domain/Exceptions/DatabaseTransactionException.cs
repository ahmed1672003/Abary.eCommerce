namespace eCommerce.Domain.Exceptions;

public sealed class DatabaseTransactionException : Exception
{
    public DatabaseTransactionException()
        : base("Transaction Does Not Completed Exception") { }

    public DatabaseTransactionException(string message = "Transaction Does Not Completed")
        : base(message) { }

    public DatabaseTransactionException(
        string message = "Transaction Does Not Completed",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
