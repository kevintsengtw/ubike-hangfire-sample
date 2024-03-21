namespace UBike.Common.Settings;

/// <summary>
/// Class DatabaseConnectionOptions
/// </summary>
public class DatabaseConnectionOptions
{
    /// <summary>
    /// ConnectionString
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Hangfire ConnectionString
    /// </summary>
    public string HangfireConnectionString { get; set; }
}