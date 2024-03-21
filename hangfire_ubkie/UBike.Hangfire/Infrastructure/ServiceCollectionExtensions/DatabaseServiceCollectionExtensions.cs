using System;
using UBike.Common.Settings;

namespace UBike.Hangfire.Infrastructure.ServiceCollectionExtensions;

/// <summary>
/// class DatabaseServiceCollectionExtensions
/// </summary>
public static class DatabaseServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Database Connection Settings.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddDatabaseConnectionOptions(this IServiceCollection services)
    {
        services.AddOptions<DatabaseConnectionOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    var sampleConnectionString = configuration.GetConnectionString("sample");
                    var hangfireConnectionString = configuration.GetConnectionString("hangfire");

                    options.ConnectionString = sampleConnectionString;
                    options.HangfireConnectionString = hangfireConnectionString;
                });

        return services;
    }
}