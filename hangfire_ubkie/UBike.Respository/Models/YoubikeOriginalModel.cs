using System.Text.Json;
using System.Text.Json.Serialization;

namespace UBike.Respository.Models;

/// <summary>
/// class YoubikeStationModel
/// </summary>
/// <remarks>
/// Youbike 原始資料
/// </remarks>
public class YoubikeOriginalModel
{
    /// <summary>
    /// The return code (retCode).
    /// </summary>
    [JsonPropertyName("retCode")]
    public int ReturnCode { get; set; }

    /// <summary>
    /// The return value (retVal).
    /// </summary>
    [JsonPropertyName("retVal")]
    public JsonElement ReturnValue { get; set; }
}