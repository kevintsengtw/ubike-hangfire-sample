namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// interface IYoubikeJob
/// </summary>
public interface IYoubikeJob
{
    /// <summary>
    /// 更新 YouBike 資料.
    /// </summary>
    Task UpdateYoubikeDataAsync();
}