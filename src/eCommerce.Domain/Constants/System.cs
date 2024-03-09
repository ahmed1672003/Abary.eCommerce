namespace eCommerce.Domain.Constants;

public static class SystemConstants
{
    public const string SYSTEM_KEY = "105246A4-335D-40EA-BCDF-755BD305278C";

    public static class Developer
    {
        public const string USER_NAME = "ahmedadel1672003";
        public const string EMAIL = "ahmedadel1672003@gmail.com";
        public const string PHONE_NUMBER = "01018856093";
        public const string PASSWORD = "Developer#123";
    }

    public static class Database
    {
        public static class Localhost
        {
            private const string DATABASE_HOST = "localhost";
            private const string DATABASE_PORT = "5432";
            private const string DATABASE_NAME = "Abary_eCommerce";
            private const string DATABASE_USER_ID = "postgres";
            private const string DATABASE_PASSWORD = "Ahmed#01280755031";
            private const string DATABASE_SSL_MODE = "prefer";
            private const string DATABASE_TIME_OUT = "30";
            public const string LOCALHOST_DATABASE_CONNECTION_STRING =
                $"host={DATABASE_HOST};port={DATABASE_PORT};Database={DATABASE_NAME};User Id={DATABASE_USER_ID};password={DATABASE_PASSWORD};sslmode={DATABASE_SSL_MODE};timeout={DATABASE_TIME_OUT}";
        }

        public static class Cloud
        {
            private const string PGHOST = "ep-mute-night-a5kakrpu.us-east-2.aws.neon.tech";
            private const string PGDATABASE = "eCommerce";
            private const string PGUSER = "ahmedadel1672003";
            private const string PGPASSWORD = "zwD9olO5bHCj";

            public const string CLOUD_DATABASE_CONNECTION_STRING =
                $"host={PGHOST};Database={PGDATABASE};User ID={PGUSER};password={PGPASSWORD}";
        }

        public static void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable(
                nameof(Cloud.CLOUD_DATABASE_CONNECTION_STRING),
                Cloud.CLOUD_DATABASE_CONNECTION_STRING
            );

            Environment.SetEnvironmentVariable(
                nameof(Localhost.LOCALHOST_DATABASE_CONNECTION_STRING),
                Localhost.LOCALHOST_DATABASE_CONNECTION_STRING
            );
        }
    }

    public static class JWT
    {
        public const string JWT_SECRET_KEY =
            "I4Uv6E3+xK4pt07YHpN4UX8xLpWghBPPr6VLPBxYIJY=I4Uv6E3+xK4pt07YHpN4UX8xLpWghBPPr6VLPBxYIJY=";
        public const string JWT_ISSURE = "Abary_eCommerce";
        public const string JWT_AUDIENCE = "Abary_eCommerce";
        public const bool JWT_VALIDATE_AUDIENCE = true;
        public const bool JWT_VALIDATE_ISSUER = true;
        public const bool JWT_VALIDATE_LIFETIME = true;
        public const bool JWT_VALIDATE_ISSUER_SIGN_IN_KEY = true;
        public const long JWT_ACCESS_TOKEN_EXPIRES_IN = 3600;

        public static void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET_KEY", $"{JWT_SECRET_KEY}");
            Environment.SetEnvironmentVariable("JWT_ISSURE", $"{JWT_ISSURE}");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", $"{JWT_AUDIENCE}");
            Environment.SetEnvironmentVariable("JWT_VALIDATE_AUDIENCE", $"{JWT_VALIDATE_AUDIENCE}");
            Environment.SetEnvironmentVariable("JWT_VALIDATE_ISSUER", $"{JWT_VALIDATE_ISSUER}");
            Environment.SetEnvironmentVariable("JWT_VALIDATE_LIFETIME", $"{JWT_VALIDATE_LIFETIME}");
            Environment.SetEnvironmentVariable(
                "JWT_VALIDATE_ISSUER_SIGN_IN_KEY",
                $"{JWT_VALIDATE_ISSUER_SIGN_IN_KEY}"
            );
            Environment.SetEnvironmentVariable(
                "JWT_ACCESS_TOKEN_EXPIRES_IN",
                $"{JWT_ACCESS_TOKEN_EXPIRES_IN}"
            );
        }
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

                public static string Delete =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Identity}.{FeatureName.User}.{PermissionValue.GetAll}";
            }
        }

        public static class Inventory
        {
            public static class Units
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Unit}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Unit}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Unit}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Unit}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Unit}.{PermissionValue.GetAll}";
            }

            public static class Categories
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Category}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Category}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Category}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Category}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Category}.{PermissionValue.GetAll}";
            }

            public static class Feature
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Feature}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Feature}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Feature}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Feature}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Feature}.{PermissionValue.GetAll}";
            }

            public static class Invoices
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Invoice}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Invoice}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Invoice}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Invoice}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Invoice}.{PermissionValue.GetAll}";
            }

            public static class Items
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Item}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Item}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Item}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Item}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Item}.{PermissionValue.GetAll}";
            }

            public static class Products
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Product}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Product}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Product}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Product}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Product}.{PermissionValue.GetAll}";
            }

            public static class Services
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Service}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Service}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Service}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Service}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Service}.{PermissionValue.GetAll}";
            }

            public static class Stocks
            {
                public static string Create =>
                    $"{ModuleName.Inventory}.{FeatureName.Stock}.{PermissionValue.Create}";

                public static string Update =>
                    $"{ModuleName.Inventory}.{FeatureName.Stock}.{PermissionValue.Update}";

                public static string Delete =>
                    $"{ModuleName.Inventory}.{FeatureName.Stock}.{PermissionValue.Delete}";

                public static string Get =>
                    $"{ModuleName.Inventory}.{FeatureName.Stock}.{PermissionValue.Get}";

                public static string GetAll =>
                    $"{ModuleName.Inventory}.{FeatureName.Stock}.{PermissionValue.GetAll}";
            }
        }
    }

    public static void SetEnvironmentVariables()
    {
        JWT.SetEnvironmentVariables();
        Database.SetEnvironmentVariables();
    }
}
