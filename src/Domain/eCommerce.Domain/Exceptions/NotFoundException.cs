namespace eCommerce.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base("Entity Not Found Exception") { }

    public NotFoundException(string message = "Entity Not Found Exception")
        : base(message) { }

    public NotFoundException(
        string message = "Entity Not Found Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
