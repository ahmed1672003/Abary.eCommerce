namespace eCommerce.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("User Unauthorized Exception") { }

    public UnauthorizedException(string message = "User Unauthorized Exception")
        : base(message) { }

    public UnauthorizedException(
        string message = "User Unauthorized Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
