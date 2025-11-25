using Models.BaseModels;

namespace Models.Entities
{
    public class City : BaseClass
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<AirQualityMeasurement> Measurements { get; set; }
    }
}
