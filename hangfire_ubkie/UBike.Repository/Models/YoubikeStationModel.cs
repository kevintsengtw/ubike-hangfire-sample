using System.Text.Json.Serialization;

namespace UBike.Repository.Models;

/// <summary>
/// class YoubikeStationModel
/// </summary>
/// <remarks>
/// Ubike 場站資料
/// </remarks>
public class YoubikeStationModel
{
    /// <summary>
    /// 站點代號
    /// </summary>
    [JsonPropertyName("sno")]
    public string No { get; set; }

    /// <summary>
    /// 場站名稱(中文)
    /// </summary>
    [JsonPropertyName("sna")]
    public string Name { get; set; }

    /// <summary>
    /// 場站總停車格
    /// </summary>
    [JsonPropertyName("tot")]
    public string Total { get; set; }

    /// <summary>
    /// 場站目前車輛數量
    /// </summary>
    [JsonPropertyName("sbi")]
    public string Bikes { get; set; }

    /// <summary>
    /// 場站區域(中文)
    /// </summary>
    [JsonPropertyName("sarea")]
    public string Area { get; set; }

    /// <summary>
    /// 資料更新時間
    /// </summary>
    [JsonPropertyName("mday")]
    public string ModifyTime { get; set; }

    /// <summary>
    /// 緯度
    /// </summary>
    [JsonPropertyName("lat")]
    public string Latitude { get; set; }

    /// <summary>
    /// 經度
    /// </summary>
    [JsonPropertyName("lng")]
    public string Longitude { get; set; }

    /// <summary>
    /// 地址(中文)
    /// </summary>
    [JsonPropertyName("ar")]
    public string Address { get; set; }

    /// <summary>
    /// 場站區域(英文)
    /// </summary>
    [JsonPropertyName("sareaen")]
    public string AreaEnglish { get; set; }

    /// <summary>
    /// 場站名稱(英文)
    /// </summary>
    [JsonPropertyName("snaen")]
    public string NameEnglish { get; set; }

    /// <summary>
    /// 地址(英文)
    /// </summary>
    [JsonPropertyName("aren")]
    public string AddressEnglish { get; set; }

    /// <summary>
    /// 空位數量
    /// </summary>
    [JsonPropertyName("bemp")]
    public string BikeEmpty { get; set; }

    /// <summary>
    /// 禁用狀態 (0:禁用, 1:正常)
    /// </summary>
    [JsonPropertyName("act")]
    public string Active { get; set; }
}