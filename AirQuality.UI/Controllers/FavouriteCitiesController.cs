using AirQuality.UI.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.IServices;
using Models.Requests.UserFavouriteCities;

namespace AirQuality.UI.Controllers
{
    public class UserFavouriteCitiesController : BaseCRUDController<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
        protected readonly IUserFavouriteCities _userFavouriteCities;
        public UserFavouriteCitiesController(IUserFavouriteCities userFavouriteCities) : base(userFavouriteCities)
        {

        }
    }
}
