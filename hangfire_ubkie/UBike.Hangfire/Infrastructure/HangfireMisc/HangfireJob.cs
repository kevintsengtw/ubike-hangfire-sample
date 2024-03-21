using System;
using Hangfire.Server;
using UBike.Service.Interface;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class HangfireJob
/// </summary>
/// <seealso cref="IHangfireJob" />
public class HangfireJob : IHangfireJob
{
    private readonly IYoubikeOpenDataService _youbikeOpenDataService;

    private readonly IYoubikeService _youbikeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HangfireJob"/> class
    /// </summary>
    /// <param name="youbikeOpenDataService">The youbike open data service</param>
    /// <param name="youbikeService">The youbike service</param>
    public HangfireJob(IYoubikeOpenDataService youbikeOpenDataService,
                       IYoubikeService youbikeService)
    {
        this._youbikeOpenDataService = youbikeOpenDataService;
        this._youbikeService = youbikeService;
    }

    /// <summary>
    /// 更新 YouBike 資料.
    /// </summary>
    public async Task UpdateYoubikeDataAsync(PerformContext context)
    {
        // 取得 Youbike 原始資料
        var sourceData = await this._youbikeOpenDataService.GetSourceDataAsync();

        // 儲存至資料庫
        this._youbikeService.UpdateData(sourceData);
    }
}