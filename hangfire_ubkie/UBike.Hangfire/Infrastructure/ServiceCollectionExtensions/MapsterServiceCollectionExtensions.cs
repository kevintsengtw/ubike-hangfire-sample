using System;
using Mapster;
using MapsterMapper;
using UBike.Service.Mapping;

namespace UBike.Hangfire.Infrastructure.ServiceCollectionExtensions;

/// <summary>
/// Class MapSterServiceCollectionExtensions
/// </summary>
public static class MapsterServiceCollectionExtensions
{
    /// <summary>
    /// Add MapSter
    /// </summary>
    /// <param name="services">services</param>
    /// <returns></returns>
    public static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        var serviceAssembly = typeof(ServiceMapRegister).Assembly;

        config.Scan(serviceAssembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}