namespace eCommerce.Domain.Exceptions;

public class DatabaseConflictException : Exception
{
    public DatabaseConflictException()
        : base("Database Conflict Exception") { }

    public DatabaseConflictException(string message = "Database Conflict Exception")
        : base(message) { }

    public DatabaseConflictException(
        string message = "Database Conflict Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
