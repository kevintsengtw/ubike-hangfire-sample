using System.Text.Json;
using Microsoft.Extensions.Options;
using UBike.Common.Settings;
using UBike.Respository.Interface;
using UBike.Respository.Models;

namespace UBike.Respository.Implement;

/// <summary>
/// class YouBikeHttpClient
/// </summary>
public class YouBikeHttpClient : IYouBikeHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly YouBikeDataSourceOptions _youBikeDataSourceOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="YouBikeHttpClient"/> class
    /// </summary>
    /// <param name="httpClient">The httpClient</param>
    /// <param name="youBikeDataSourceOptions">The youBikeDataSourceOptions</param>
    public YouBikeHttpClient(HttpClient httpClient,
                             IOptions<YouBikeDataSourceOptions> youBikeDataSourceOptions)
    {
        this._httpClient = httpClient;
        this._youBikeDataSourceOptions = youBikeDataSourceOptions.Value;
    }

    /// <summary>
    /// 取得 Youbike 原始資料
    /// </summary>
    /// <returns></returns>
    public async Task<YoubikeOriginalModel> GetSourceDataAsync()
    {
        var response = await this._httpClient.GetAsync(this._youBikeDataSourceOptions.DataSource);
        var responseData = await response.Content.ReadAsStringAsync();
        var originalData = JsonSerializer.Deserialize<YoubikeOriginalModel>(responseData);
        return originalData;
    }
}