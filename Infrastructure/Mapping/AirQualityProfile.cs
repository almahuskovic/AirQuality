using AutoMapper;
using Models.Dto;
using Models.Entities;
using Models.Requests.Cities;
using Models.Requests.UserFavouriteCities;

namespace Infrastructure.Mapping
{
    public class AirQualityProfile : Profile
    {
        public AirQualityProfile()
        {

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<CityUpsertRequest, City>();

            CreateMap<AirQualityMeasurement, AirQualityMeasurementDto>().ReverseMap();

            CreateMap<FavouriteCities, FavouriteCitiesDto>().ReverseMap();
            CreateMap<FavouriteCitiesSearchRequest, FavouriteCities>();
        }
    }
}
