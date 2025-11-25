using AutoMapper;
using Infrastructure.BaseServices;
using Models.Dto;
using Models.Entities;
using Models.IServices;
using Models.Requests.Cities;

namespace Infrastructure.Services
{
    public class CityService : BaseCRUDService<CityDto, City, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>, ICity
    {
        public CityService(Context context, IMapper mapper) : base(context, mapper)
        {

        }

        public override IEnumerable<CityDto> Get(CitySearchRequest search = null)
        {

            return base.Get(search);
        }
    }
}
