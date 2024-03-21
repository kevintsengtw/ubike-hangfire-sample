using System;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// interface IHangfireJobTrigger
/// </summary>
public interface IHangfireJobTrigger
{
    /// <summary>
    /// Called when [start].
    /// </summary>
    void OnStart();
}