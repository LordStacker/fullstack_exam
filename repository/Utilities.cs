namespace repository;

public class Utilities
{
    public static string? ProperlyFormattedConnectionString;

    public static string FormatConnectionString(string connectionString)
    {
        Uri uri = new(connectionString);
        ProperlyFormattedConnectionString = string.Format(
            "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=2",
            uri.Host,
            uri.AbsolutePath.Trim('/'),
            uri.UserInfo.Split(':')[0],
            uri.UserInfo.Split(':')[1],
            uri.Port > 0 ? uri.Port : 5432);
        return ProperlyFormattedConnectionString;
    }
}