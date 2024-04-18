using MapsterMapper;
using Throw;
using UBike.Repository.Interface;
using UBike.Repository.Models;
using UBike.Service.Dto;
using UBike.Service.Interface;

// ReSharper disable PossibleMultipleEnumeration

namespace UBike.Service.Implement;

/// <summary>
/// class YoubikeService
/// </summary>
public class YoubikeService : IYoubikeService
{
    private readonly IMapper _mapper;

    private readonly IStationRepository _stationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="YoubikeService"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    /// <param name="stationRepository">The station repository.</param>
    public YoubikeService(IMapper mapper,
                          IStationRepository stationRepository)
    {
        this._mapper = mapper;
        this._stationRepository = stationRepository;
    }

    /// <summary>
    /// 更新 YouBike 的 Stattion 與 StationLocation 資料.
    /// </summary>
    /// <param name="sourceData">The source data.</param>
    /// <returns></returns>
    public async Task UpdateDataAsync(IEnumerable<OriginalStationDto> sourceData)
    {
        if (sourceData is null || !sourceData.Any())
        {
            return;
        }

        var stationModels = this._mapper.Map<IEnumerable<StationModel>>(sourceData);

        // 整批刪除
        var stationNumbers = stationModels.Select(x => x.StationNo);
        await this._stationRepository.BulkDeleteAsync(stationNumbers);

        // 整批新增
        await this._stationRepository.BulkInsertAsync(stationModels);
    }

    /// <summary>
    /// 取得所有的 YouBike 場站資料.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    public async Task<IEnumerable<StationDto>> GetAllStationsAsync(int from, int size)
    {
        from.Throw().IfLessThanOrEqualTo(0);
        size.Throw().IfLessThanOrEqualTo(0);

        var stationAmount = await this._stationRepository.GetTotalCountAsync();
        if (stationAmount == 0)
        {
            return Enumerable.Empty<StationDto>();
        }

        var stationModels = await this._stationRepository.GetRangeAsync(from, size);

        var stations = this._mapper.Map<IEnumerable<StationDto>>(stationModels);
        return stations;
    }

    /// <summary>
    /// 取得指定區域、數量、範圍的 YouBike 場站資料.
    /// </summary>
    /// <param name="area">The area.</param>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    public async Task<IEnumerable<StationDto>> GetStationsAsync(string area, int from, int size)
    {
        area.Throw().IfNullOrWhiteSpace(x => x);
        from.Throw().IfLessThanOrEqualTo(0);
        size.Throw().IfLessThanOrEqualTo(0);

        var amount = await this._stationRepository.GetCountByAreaAsync(area);
        if (amount == 0)
        {
            return Enumerable.Empty<StationDto>();
        }

        var stationModels = await this._stationRepository.QueryByAreaAsync(area, from, size);

        var stations = this._mapper.Map<IEnumerable<StationDto>>(stationModels);
        return stations;
    }
}