namespace eCommerce.Domain.Constants;

public static class SystemConstants
{
    public const string SYSTEM_KEY = "105246A4-335D-40EA-BCDF-755BD305278C";

    public static class Developer
    {
        public const string USER_NAME = "ahmedadel1672003";
        public const string EMAIL = "ahmedadel1672003@gmail.com";
        public const string PHONE_NUMBER = "01018856093";
        public const string PASSWORD = "Ahmed#01280755031";
    }

    public static class Security
    {
        public static class Shared
        {
            public static class File
            {
                public static string Create =>
                    $"{ModuleName.Shared}.{FeatureName.File}.{PermissionValue.Create}";
                public static string Update =>
                    $"{ModuleName.Shared}.{FeatureName.File}.{PermissionValue.Update}";
                public static string Get =>
                    $"{ModuleName.Shared}.{FeatureName.File}.{PermissionValue.Get}";
                public static string GetAll =>
                    $"{ModuleName.Shared}.{FeatureName.File}.{PermissionValue.GetAll}";
            }
        }

        public static class Identity
        {
            public static class Users
            {
                public static string Create =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.Create}";
                public static string Update =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.Update}";
                public static string Get =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.Get}";
                public static string GetAll =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.GetAll}";
            }
        }
    }
}
