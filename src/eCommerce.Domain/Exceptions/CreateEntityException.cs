namespace eCommerce.Domain.Exceptions;

public class CreateEntityException : Exception
{
    public CreateEntityException()
        : base("Entity Can Not Created") { }

    public CreateEntityException(string message = "Entity Can Not Created")
        : base(message) { }

    public CreateEntityException(
        string message = "Entity Can Not Created",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
