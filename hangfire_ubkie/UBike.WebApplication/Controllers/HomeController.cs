using System;
using Microsoft.AspNetCore.Mvc;
using UBike.Service.Interface;

namespace UBike.WebApplication.Controllers;

/// <summary>
/// class HomeController
/// </summary>
/// <remarks>
/// 1.取得所有 Youbike 資料，要有分頁功能
/// 2.輸入區域名稱，取得該區域的全部 Youbike 資料，要有分頁功能
/// </remarks>
[Route("api")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IYoubikeService _youbikeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HomeController"/> class
    /// </summary>
    /// <param name="youbikeService">The youbike service</param>
    public HomeController(IYoubikeService youbikeService)
    {
        this._youbikeService = youbikeService;
    }

    /// <summary>
    /// 取得所有 Youbike 資料
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    [HttpGet("all")]
    public IActionResult GetAll([FromQuery] int from, [FromQuery] int size)
    {
        // api/all.GET

        var youbikes = this._youbikeService.GetAllStations(from, size);
        return this.Ok(youbikes);
    }

    /// <summary>
    /// 輸入區域名稱，取得該區域的全部 Youbike 資料
    /// </summary>
    /// <param name="area">The area.</param>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    [HttpGet("area")]
    public IActionResult GetByArea([FromQuery] string area, [FromQuery] int from, [FromQuery] int size)
    {
        // api/area.GET

        var youbikes = this._youbikeService.GetStations(area, from, size);
        return this.Ok(youbikes);
    }
}