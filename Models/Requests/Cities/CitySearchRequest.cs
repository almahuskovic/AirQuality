using Models.BaseModels;

namespace Models.Requests.Cities
{
    public class CitySearchRequest : BaseSearchObject
    {
        public string? Name { get; set; }
        public string? ISO { get; set; }
        public double? East { get; set; }
        public double? West { get; set; }
        public double? North { get; set; }
        public double? South { get; set; }
        public bool? SkipFavourites { get; set; }
    }
}
