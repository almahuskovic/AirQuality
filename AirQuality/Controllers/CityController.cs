using AirQuality.Controllers.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.IServices;
using Models.Requests.Cities;

namespace AirQuality.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class CityController : BaseCRUDController<CityDto, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        protected readonly ICity _cityService;

        public CityController(ICity cityService) : base(cityService)
        {
            _cityService = cityService;
        }

        
    }
}
