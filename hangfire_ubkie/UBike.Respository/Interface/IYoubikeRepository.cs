﻿using UBike.Respository.Models;

namespace UBike.Respository.Interface;

/// <summary>
/// interface IYoubikeRepository
/// </summary>
public interface IYoubikeRepository
{
    /// <summary>
    /// 取得 YouBike 場站資料.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<YoubikeStationModel>> GetStationsAsync();
}