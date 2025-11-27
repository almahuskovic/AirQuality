using Models.BaseModels;

namespace Models.Requests.Cities
{
    public class CitySearchRequest : BaseSearchObject
    {
        public string Name { get; set; }
        public string? Country { get; set; }
    }
}
