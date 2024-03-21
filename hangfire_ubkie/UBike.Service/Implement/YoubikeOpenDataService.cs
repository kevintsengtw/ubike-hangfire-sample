using System;
using MapsterMapper;
using UBike.Respository.Interface;
using UBike.Service.Dto;
using UBike.Service.Interface;

namespace UBike.Service.Implement;

/// <summary>
/// class YoubikeOpenDataService
/// </summary>
/// <seealso cref="IYoubikeOpenDataService" />
public class YoubikeOpenDataService : IYoubikeOpenDataService
{
    private readonly IMapper _mapper;

    private readonly IYoubikeRepository _youbikeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="YoubikeOpenDataService" /> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    /// <param name="youbikeRepository">The youbikeRepository.</param>
    public YoubikeOpenDataService(IMapper mapper,
                                  IYoubikeRepository youbikeRepository)
    {
        this._mapper = mapper;
        this._youbikeRepository = youbikeRepository;
    }

    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// 取得原始的 YouBike 公開資料.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<OriginalStationDto>> GetSourceDataAsync()
    {
        var youbikeStations = await this._youbikeRepository.GetStationsAsync();

        if (youbikeStations is null || !youbikeStations.Any())
        {
            return new List<OriginalStationDto>();
        }

        var result = this._mapper.Map<IEnumerable<OriginalStationDto>>(youbikeStations);
        return result;
    }
}