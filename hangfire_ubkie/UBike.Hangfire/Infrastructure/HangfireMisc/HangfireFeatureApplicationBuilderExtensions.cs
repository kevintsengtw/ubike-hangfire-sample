using Hangfire;
using Hangfire.Storage;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class HangfireFeatureApplicationBuilderExtensions
/// </summary>
public static class HangfireFeatureApplicationBuilderExtensions
{
    /// <summary>
    /// Add the recurring jobs.
    /// </summary>
    /// <param name="app">The app</param>
    /// <returns>The app</returns>
    public static IApplicationBuilder AddRecurringJobs(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        
        // 清除已有的排程工作
        using (var connection = JobStorage.Current.GetConnection())
        {
            foreach (var recurringJob in connection.GetRecurringJobs())
            {
                RecurringJob.RemoveIfExists(recurringJob.Id);
            }
        }

        var recurringJobs = serviceProvider.GetRequiredService<IEnumerable<IRecurringJob>>();
        
        // 加入排程工作
        foreach (var recurringJob in recurringJobs)
        {
            RecurringJob.AddOrUpdate(
                recurringJobId: recurringJob.JobId,
                cronExpression: recurringJob.CronExpression,
                options: recurringJob.RecurringJobOptions,
                methodCall: () => recurringJob.ExecuteAsync());
        }        

        return app;
    }
}