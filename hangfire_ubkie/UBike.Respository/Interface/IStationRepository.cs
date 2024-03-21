﻿using UBike.Common.Misc;
using UBike.Respository.Models;

namespace UBike.Respository.Interface;

/// <summary>
/// interface IStationRepository
/// </summary>
public interface IStationRepository
{
    /// <summary>
    /// 指定場站代號的資料是否存在.
    /// </summary>
    /// <param name="stationNo">場站代號</param>
    /// <returns>
    ///   <c>true</c> if the specified station no is exists; otherwise, <c>false</c>.
    /// </returns>
    bool IsExists(string stationNo);

    /// <summary>
    /// 依據場站代號取得 Station 資料.
    /// </summary>
    /// <param name="stationNo">場站代號</param>
    /// <returns></returns>
    StationModel Get(string stationNo);

    /// <summary>
    /// 取得指定範圍的 Station 資料.
    /// </summary>
    /// <param name="from">from</param>
    /// <param name="size">size</param>
    /// <returns></returns>
    IEnumerable<StationModel> GetRange(int from, int size);

    /// <summary>
    /// 取得全部 station 資料數量.
    /// </summary>
    /// <returns></returns>
    int GetTotalCount();

    /// <summary>
    /// 新增 station 資料.
    /// </summary>
    /// <param name="model">Station</param>
    /// <returns></returns>
    IResult Insert(StationModel model);

    /// <summary>
    /// 新增多筆 Station 資料.
    /// </summary>
    /// <param name="models">The models.</param>
    /// <returns></returns>
    IResult BulkInsert(IEnumerable<StationModel> models);

    /// <summary>
    /// 刪除指定的 station 資料.
    /// </summary>
    /// <param name="stationNo">The stationNo.</param>
    /// <returns></returns>
    IResult Delete(string stationNo);

    /// <summary>
    /// 刪除多筆的 station 資料.
    /// </summary>
    /// <param name="stationNumbers">The stationNumbers.</param>
    /// <returns></returns>
    IResult BulkDelete(IEnumerable<string> stationNumbers);

    /// <summary>
    /// 以 area 查詢並取得 staion 資料.
    /// </summary>
    /// <param name="area">The area.</param>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    IEnumerable<StationModel> QueryByArea(string area, int from, int size);

    /// <summary>
    /// 以 area 查詢並取得 station 資料筆數.
    /// </summary>
    /// <param name="area">The area.</param>
    /// <returns></returns>
    int GetCountByArea(string area);
}