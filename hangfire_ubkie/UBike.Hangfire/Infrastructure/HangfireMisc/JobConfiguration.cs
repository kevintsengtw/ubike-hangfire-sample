namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class JobConfiguration
/// </summary>
public class JobConfiguration
{
    private readonly IServiceCollection _services;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobConfiguration"/> class
    /// </summary>
    /// <param name="services">The services</param>
    internal JobConfiguration(IServiceCollection services)
    {
        this._services = services;
    }

    /// <summary>
    /// Adds the RecurringJob.
    /// </summary>
    /// <typeparam name="TJob"></typeparam>
    /// <returns></returns>
    public JobConfiguration AddRecurringJob<TJob>() where TJob : IRecurringJob
    {
        this._services.AddScoped(typeof(IRecurringJob), typeof(TJob));
        return this;
    }
}