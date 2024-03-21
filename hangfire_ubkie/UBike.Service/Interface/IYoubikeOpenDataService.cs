using System;
using UBike.Service.Dto;

namespace UBike.Service.Interface;

/// <summary>
/// interface IYoubikeOpenDataService
/// </summary>
public interface IYoubikeOpenDataService
{
    /// <summary>
    /// 取得原始的 YouBike 公開資料.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<OriginalStationDto>> GetSourceDataAsync();
}