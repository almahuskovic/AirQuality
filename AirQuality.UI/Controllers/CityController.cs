using AirQuality.UI.Controllers.BaseControllers;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.Requests.Cities;

namespace AirQuality.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : BaseCRUDController<CityDto, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        public CityController(ICityService cityService) : base(cityService)
        {
        }


    }
}
