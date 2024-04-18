using System;
using UBike.Service.Dto;

namespace UBike.Service.Interface;

/// <summary>
/// interface IYoubikeService
/// </summary>
public interface IYoubikeService
{
    /// <summary>
    /// 更新 YouBike 的 Stattion 與 StationLocation 資料.
    /// </summary>
    /// <param name="sourceData">The source data.</param>
    /// <returns></returns>
    Task UpdateDataAsync(IEnumerable<OriginalStationDto> sourceData);

    /// <summary>
    /// 取得所有的 YouBike 場站資料.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    Task<IEnumerable<StationDto>> GetAllStationsAsync(int from, int size);

    /// <summary>
    /// 取得指定區域、數量、範圍的 YouBike 場站資料.
    /// </summary>
    /// <param name="area">The area.</param>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    Task<IEnumerable<StationDto>> GetStationsAsync(string area, int from, int size);
}