namespace eCommerce.Presentation.Messages;

public static class ErrorMessage
{
    private const string Required = "مطلوب";
    private const string Exist = "موجود من قبل";

    public static class Users
    {
        public const string UserNameRequired = $"{Required} اسم المستخدم";
        public const string EmailRequired = $"email {Required}";
        public const string Password = $"password {Required}";
        public const string ConfirmPassword = $"confirm password {Required}";
        public const string PasswordDoConfirmPassword = $"confirm password {Required}";
        public const string UserNameOrEmailExist = $"user name or email {Exist}";
    }
}
