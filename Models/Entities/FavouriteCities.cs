using Models.BaseModels;

namespace Models.Entities
{
    public class FavouriteCities : BaseClass
    {
        public int CityId { get; set; }
        public City City { get; set; }
     
        public string UserId { get; set; }
    }
}
