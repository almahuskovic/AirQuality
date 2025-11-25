using Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class AirQualityMeasurement : BaseClass
    {
        public double PM25 { get; set; }
        public double PM10 { get; set; }
        public double NO2 { get; set; }
        public DateTime MeasuredAt { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }
    }
}
