using Hangfire;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// interface IRecurringJob
/// </summary>
public interface IRecurringJob
{
    string JobId { get; }

    string CronExpression { get; }

    RecurringJobOptions RecurringJobOptions { get; }

    Task ExecuteAsync();
}