namespace eCommerce.Domain.Exceptions;

public sealed class DatabaseTransactionException : Exception
{
    public DatabaseTransactionException()
        : base("Database Transaction Does Not Completed Exception") { }

    public DatabaseTransactionException(
        string message = "Database Transaction Does Not Completed Exception"
    )
        : base(message) { }

    public DatabaseTransactionException(
        string message = "Database Transaction Does Not Completed Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
