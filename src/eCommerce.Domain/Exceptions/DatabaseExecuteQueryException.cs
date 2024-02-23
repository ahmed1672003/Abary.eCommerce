namespace eCommerce.Domain.Exceptions;

public sealed class DatabaseExecuteQueryException : Exception
{
    public DatabaseExecuteQueryException()
        : base("Database Excecute Query Exception") { }

    public DatabaseExecuteQueryException(string message = "Database Excecute Query Exception")
        : base(message) { }

    public DatabaseExecuteQueryException(
        string message = "Database Excecute Query Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
