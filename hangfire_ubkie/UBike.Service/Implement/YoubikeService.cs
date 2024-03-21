using MapsterMapper;
using Throw;
using UBike.Respository.Interface;
using UBike.Respository.Models;
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
    public void UpdateData(IEnumerable<OriginalStationDto> sourceData)
    {
        if (sourceData is null || !sourceData.Any())
        {
            return;
        }

        var stationModels = this._mapper.Map<IEnumerable<StationModel>>(sourceData);

        // 整批刪除
        var stationNumbers = stationModels.Select(x => x.StationNo);
        this._stationRepository.BulkDelete(stationNumbers);

        // 整批新增
        this._stationRepository.BulkInsert(stationModels);
    }

    /// <summary>
    /// 取得所有的 YouBike 場站資料.
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    public IEnumerable<StationDto> GetAllStations(int from, int size)
    {
        from.Throw().IfLessThanOrEqualTo(0);
        size.Throw().IfLessThanOrEqualTo(0);

        var stationAmount = this._stationRepository.GetTotalCount();
        if (stationAmount == 0)
        {
            return Enumerable.Empty<StationDto>();
        }

        var stationModels = this._stationRepository.GetRange(from, size);

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
    public IEnumerable<StationDto> GetStations(string area, int from, int size)
    {
        area.Throw().IfNullOrWhiteSpace(x => x);
        from.Throw().IfLessThanOrEqualTo(0);
        size.Throw().IfLessThanOrEqualTo(0);

        var amount = this._stationRepository.GetCountByArea(area);
        if (amount == 0)
        {
            return Enumerable.Empty<StationDto>();
        }

        var stationModels = this._stationRepository.QueryByArea(area, from, size);

        var stations = this._mapper.Map<IEnumerable<StationDto>>(stationModels);
        return stations;
    }
}