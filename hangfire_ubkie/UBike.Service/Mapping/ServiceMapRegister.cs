using System.Globalization;
using Mapster;
using UBike.Repository.Models;
using UBike.Service.Dto;

namespace UBike.Service.Mapping;

/// <summary>
/// class MappingProfile
/// </summary>
public class ServiceMapRegister : IRegister
{
    private const string DateTimeFormat = "yyyyMMddHHmmssfff";

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<YoubikeStationModel, OriginalStationDto>()
              .Map(d => d.StationNo, s => s.No)
              .Map(d => d.StationName, s => s.Name)
              .Map(d => d.Total, s => s.Total)
              .Map(d => d.StationBikes, s => s.Bikes)
              .Map(d => d.StationArea, s => s.Area)
              .Map(d => d.ModifyTime, s => s.ModifyTime)
              .Map(d => d.Latitude, s => s.Latitude)
              .Map(d => d.Longitude, s => s.Longitude)
              .Map(d => d.Address, s => s.Address)
              .Map(d => d.StationAreaEnglish, s => s.AreaEnglish)
              .Map(d => d.StationNameEnglish, s => s.NameEnglish)
              .Map(d => d.AddressEnglish, s => s.AddressEnglish)
              .Map(d => d.BikeEmpty, s => s.BikeEmpty)
              .Map(d => d.Active, s => s.Active);

        config.NewConfig<OriginalStationDto, StationModel>()
              .Map(d => d.StationNo, s => s.StationNo)
              .Map(d => d.StationName, s => s.StationName)
              .Map(d => d.Total, s => s.Total)
              .Map(d => d.StationBikes, s => s.StationBikes)
              .Map(d => d.StationArea, s => s.StationArea)
              .Map(d => d.ModifyTime, s => s.ModifyTime)
              .Map(d => d.Latitude, s => s.Latitude)
              .Map(d => d.Longitude, s => s.Longitude)
              .Map(d => d.Address, s => s.Address)
              .Map(d => d.StationAreaEnglish, s => s.StationAreaEnglish)
              .Map(d => d.StationNameEnglish, s => s.StationNameEnglish)
              .Map(d => d.AddressEnglish, s => s.AddressEnglish)
              .Map(d => d.BikeEmpty, s => s.BikeEmpty)
              .Map(d => d.Active, s => s.Active.Equals("1"));

        config.NewConfig<StationModel, StationDto>()
              .Map(d => d.No, s => s.StationNo)
              .Map(d => d.Name, s => s.StationName)
              .Map(d => d.Total, s => s.Total)
              .Map(d => d.Bikes, s => s.StationBikes)
              .Map(d => d.Area, s => s.StationArea)
              .Map(d => d.ModifyTime,
                   s => string.IsNullOrWhiteSpace(s.ModifyTime)
                       ? DateTime.MinValue
                       : DateTime.ParseExact($"{s.ModifyTime}000", DateTimeFormat, CultureInfo.InvariantCulture))
              .Map(d => d.Latitude, s => s.Latitude)
              .Map(d => d.Longitude, s => s.Longitude)
              .Map(d => d.Address, s => s.Address)
              .Map(d => d.AreaEnglish, s => s.StationAreaEnglish)
              .Map(d => d.NameEnglish, s => s.StationNameEnglish)
              .Map(d => d.AddressEnglish, s => s.AddressEnglish)
              .Map(d => d.BikeEmpty, s => s.BikeEmpty)
              .Map(d => d.Active, s => s.Active);
    }
}