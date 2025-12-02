using Infrastructure.Interfaces.BaseServices;
using Models.Dto;
using Models.Requests.Cities;

namespace Infrastructure.Interfaces
{
    public interface ICityService : IBaseCRUDService<CityDto, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        public void ImportCitiesInDB();
    }
}
