using AutoMapper;
using Models.Dto;
using Models.Entities;
using Models.Requests.Cities;

namespace Infrastructure.Mapping
{
    public class AirQualityProfile : Profile
    {
        public AirQualityProfile()
        {

            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<CityUpsertRequest, City>();
        }
    }
}
