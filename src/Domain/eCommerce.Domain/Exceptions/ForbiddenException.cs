namespace eCommerce.Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("Forbidden Exception") { }

    public ForbiddenException(string message = "Forbidden Exception")
        : base(message) { }

    public ForbiddenException(
        string message = "Forbidden Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
