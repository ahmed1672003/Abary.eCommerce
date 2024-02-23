namespace eCommerce.Domain.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
        : base("BadRequest Exception") { }

    public BadRequestException(string message = "BadRequest Exception")
        : base(message) { }

    public BadRequestException(
        string message = "BadRequest Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
