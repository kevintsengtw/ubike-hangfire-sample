using UBike.Respository.Models;

namespace UBike.Respository.Interface;

/// <summary>
/// interface IYouBikeHttpClient
/// </summary>
public interface IYouBikeHttpClient
{
    /// <summary>
    /// 取得 Youbike 原始資料
    /// </summary>
    /// <returns></returns>
    Task<YoubikeOriginalModel> GetSourceDataAsync();
}