namespace eCommerce.Domain.Exceptions;

public class SaveChangesException : Exception
{
    public SaveChangesException()
        : base("Save Changes On Database Exception") { }

    public SaveChangesException(string message = "Save Changes On Database Exception")
        : base(message) { }

    public SaveChangesException(
        string message = "Save Changes On Database Exception",
        Exception innerException = null
    )
        : base(message, innerException) { }
}
