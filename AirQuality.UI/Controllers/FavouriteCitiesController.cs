using AirQuality.UI.Controllers.BaseControllers;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.Requests.UserFavouriteCities;

namespace AirQuality.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavouriteCitiesController : BaseCRUDController<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
        private readonly IFavouriteCitiesService _favouritecities;
        public FavouriteCitiesController(IFavouriteCitiesService favouriteCities) : base(favouriteCities)
        {
            _favouritecities = favouriteCities;
        }

        [HttpGet("GetFavouriteCities")]
        public async Task<List<FavouriteCitiesDto>?> GetFavouriteCities([FromQuery] FavouriteCitiesSearchRequest request)
        {
            return await _favouritecities.GetFavouriteCities(request);
        }
    }
}
