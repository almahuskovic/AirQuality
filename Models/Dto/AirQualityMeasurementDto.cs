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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public double Generationtime_Ms { get; set; }
        public int Utc_Offset_Seconds { get; set; }
        public string Timezone { get; set; }
        public string Timezone_Abbreviation { get; set; }
        public HourlyData Hourly { get; set; }
        public HourlyUnits Hourly_Units { get; set; }
    }

    public class HourlyData
    {
        public List<string> Time { get; set; }
        public List<double?> Pm10 { get; set; }
        public List<double?> Pm2_5 { get; set; }
        public List<double?> Ozone { get; set; }
        public List<double?> NitrogenDioxide { get; set; }
        public List<double?> SulphurDioxide { get; set; }
        public List<double?> CarbonMonoxide { get; set; }
        public List<double?> Ammonia { get; set; }
    }

    public class HourlyUnits
    {
        public string Pm10 { get; set; }
        public string Pm2_5 { get; set; }
        public string Ozone { get; set; }
        public string NitrogenDioxide { get; set; }
        public string SulphurDioxide { get; set; }
        public string CarbonMonoxide { get; set; }
        public string Ammonia { get; set; }
    }
}
