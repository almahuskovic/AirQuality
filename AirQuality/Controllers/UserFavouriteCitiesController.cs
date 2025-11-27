using AirQuality.Controllers.BaseControllers;
using Models.Dto;
using Models.IServices;
using Models.Requests.UserFavouriteCities;

namespace AirQuality.Controllers
{
    public class UserFavouriteCitiesController : BaseCRUDController<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
        protected readonly IUserFavouriteCities _userFavouriteCities;
        public UserFavouriteCitiesController(IUserFavouriteCities userFavouriteCities) : base(userFavouriteCities)
        {

        }
    }
}
