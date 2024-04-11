using Hangfire;
using UBike.Service.Interface;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class YoubikeRecurringJob
/// </summary>
public class YoubikeRecurringJob : IRecurringJob
{
    private readonly IYoubikeOpenDataService _youbikeOpenDataService;

    private readonly IYoubikeService _youbikeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="YoubikeRecurringJob"/> class
    /// </summary>
    /// <param name="youbikeOpenDataService">The youbikeOpenDataService</param>
    /// <param name="youbikeService">The youbikeService</param>
    public YoubikeRecurringJob(IYoubikeOpenDataService youbikeOpenDataService,
                               IYoubikeService youbikeService)
    {
        this._youbikeOpenDataService = youbikeOpenDataService;
        this._youbikeService = youbikeService;
    }

    /// <summary>
    /// JobId
    /// </summary>
    public string JobId => "YoubikeRecurringJob.ExecuteAsync";

    /// <summary>
    /// CronExpression
    /// </summary>
    public string CronExpression => "0/5 * * * *";

    /// <summary>
    /// RecurringJobOptions
    /// </summary>
    public RecurringJobOptions RecurringJobOptions => new() { TimeZone = TimeZoneInfo.Local };

    /// <summary>
    /// 更新 YouBike 資料.
    /// </summary>
    public async Task ExecuteAsync()
    {
        // 取得 Youbike 原始資料
        var sourceData = await this._youbikeOpenDataService.GetSourceDataAsync();

        // 儲存至資料庫
        this._youbikeService.UpdateData(sourceData);
    }
}