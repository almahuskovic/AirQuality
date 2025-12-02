using Infrastructure.Interfaces.BaseServices;
using Models.Dto;
using Models.Requests.UserFavouriteCities;

namespace Infrastructure.Interfaces
{
    public interface IFavouriteCitiesService : IBaseCRUDService<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
        Task<List<FavouriteCitiesDto>?> GetFavouriteCities(FavouriteCitiesSearchRequest request);
    }
}
