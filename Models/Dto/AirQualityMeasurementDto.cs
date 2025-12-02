using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class AirQualityMeasurementDto
    {
        public int Id{ get; set; }
        public double PM25 { get; set; }
        public double PM10 { get; set; }
        public double NO2 { get; set; }
        public int AQI { get; set; }
        public DateTime MeasuredAt { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

   
}
