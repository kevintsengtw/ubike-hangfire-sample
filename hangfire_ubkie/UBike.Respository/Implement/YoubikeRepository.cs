using System.Text.Json;
using UBike.Respository.Interface;
using UBike.Respository.Models;

namespace UBike.Respository.Implement;

/// <summary>
/// class YoubikeRepository
/// </summary>
public class YoubikeRepository : IYoubikeRepository
{
    private readonly IYouBikeHttpClient _youBikeHttpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="YoubikeRepository" /> class.
    /// </summary>
    /// <param name="youBikeHttpClient">The youBikeHttpClient</param>
    public YoubikeRepository(IYouBikeHttpClient youBikeHttpClient)
    {
        this._youBikeHttpClient = youBikeHttpClient;
    }

    /// <summary>
    /// 取得 YouBike 場站資料.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<YoubikeStationModel>> GetStationsAsync()
    {
        var sourceData = await this._youBikeHttpClient.GetSourceDataAsync();
        if (sourceData is not { ReturnCode: 1 })
        {
            return Enumerable.Empty<YoubikeStationModel>();
        }

        var result = sourceData.ReturnValue.EnumerateObject()
                               .Select(item => JsonSerializer.Deserialize<YoubikeStationModel>($"{item.Value}"));

        return result;
    }
}