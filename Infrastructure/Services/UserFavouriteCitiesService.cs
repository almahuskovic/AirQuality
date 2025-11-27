using AutoMapper;
using Infrastructure.BaseServices;
using Models.Dto;
using Models.Entities;
using Models.IServices;
using Models.Requests.UserFavouriteCities;

namespace Infrastructure.Services
{
    public class UserFavouriteCitiesService : BaseCRUDService<FavouriteCitiesDto, FavouriteCities, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>, IUserFavouriteCities
    {
        public UserFavouriteCitiesService(Context context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
