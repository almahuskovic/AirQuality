using Models.BaseModels;

namespace Models.Entities
{
    public class FavouriteCities : BaseClass
    {
        public Guid CityId { get; set; }
        public City City { get; set; }
     
        //public int UserId { get; set; }
    }
}
