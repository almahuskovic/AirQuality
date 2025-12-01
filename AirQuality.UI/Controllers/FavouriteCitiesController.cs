using AirQuality.UI.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.IServices;
using Models.Requests.UserFavouriteCities;

namespace AirQuality.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavouriteCitiesController : BaseCRUDController<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
        public FavouriteCitiesController(IFavouriteCities favouriteCities) : base(favouriteCities)
        {     
        }
    }
}
