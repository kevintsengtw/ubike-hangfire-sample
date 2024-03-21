using System;
using UBike.Common.Settings;

namespace UBike.WebApplication.Infrastructure.ServiceCollectionExtensions;

/// <summary>
/// class YouBikeDataSourceServiceCollectionExtensions
/// </summary>
public static class YouBikeDataSourceServiceCollectionExtensions
{
    /// <summary>
    /// Adds the YouBikeDataSourceOptions.
    /// </summary>
    /// <param name="services">The services</param>
    /// <returns>The services</returns>
    public static IServiceCollection AddYouBikeDataSourceOptions(this IServiceCollection services)
    {
        services.AddOptions<YouBikeDataSourceOptions>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    var youBikeDataSourceSection = configuration.GetSection("YouBikeDataSource");
                    var youBikeDataSourceOptions = youBikeDataSourceSection.Get<YouBikeDataSourceOptions>();
                    options.DataSource = youBikeDataSourceOptions.DataSource;
                });

        return services;
    }
}