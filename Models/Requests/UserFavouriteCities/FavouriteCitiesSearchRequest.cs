using Models.BaseModels;

namespace Models.Requests.UserFavouriteCities
{
    public class FavouriteCitiesSearchRequest : BaseSearchObject
    {
        public string? UserId { get; set; }
    }
}
