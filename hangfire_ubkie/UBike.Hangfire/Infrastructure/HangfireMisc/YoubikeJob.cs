using UBike.Service.Interface;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class YoubikeJob
/// </summary>
/// <seealso cref="IYoubikeJob" />
public class YoubikeJob : IYoubikeJob
{
    private readonly IYoubikeOpenDataService _youbikeOpenDataService;

    private readonly IYoubikeService _youbikeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="YoubikeJob"/> class
    /// </summary>
    /// <param name="youbikeOpenDataService">The youbikeOpenDataService</param>
    /// <param name="youbikeService">The youbikeService</param>
    public YoubikeJob(IYoubikeOpenDataService youbikeOpenDataService,
                      IYoubikeService youbikeService)
    {
        this._youbikeOpenDataService = youbikeOpenDataService;
        this._youbikeService = youbikeService;
    }

    /// <summary>
    /// 更新 YouBike 資料.
    /// </summary>
    public async Task UpdateDataAsync()
    {
        // 取得 Youbike 原始資料
        var sourceData = await this._youbikeOpenDataService.GetSourceDataAsync();

        // 儲存至資料庫
        await this._youbikeService.UpdateDataAsync(sourceData);
    }
}