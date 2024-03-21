using System;
using Hangfire.Server;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// interface IHangfireJob
/// </summary>
public interface IHangfireJob
{
    /// <summary>
    /// 更新 YouBike 資料.
    /// </summary>
    Task UpdateYoubikeDataAsync(PerformContext context);
}