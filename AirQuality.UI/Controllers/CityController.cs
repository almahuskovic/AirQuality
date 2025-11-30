using AirQuality.UI.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.IServices;
using Models.Requests.Cities;

namespace AirQuality.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class CityController : BaseCRUDController<CityDto, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
        public CityController(ICity cityService) : base(cityService)
        {
        }


    }
}
