using Models.BaseIServices;
using Models.Dto;
using Models.Requests.UserFavouriteCities;

namespace Models.IServices
{
    public interface IFavouriteCities : IBaseCRUDService<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
        Task<FavouriteCitiesDto?> GetFavouriteCities();
    }
}
