using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.IServices
{
    public interface IAirQuality
    {
        public Task<AirQualityMeasurementDto?> GetLatestByCityId(int cityId);
        public Task<List<AirQualityApiResponse>?> GetAQIByCities(string country);
        public Task<List<AirQualityApiResponse>?> GetAQIForVisibleCities(double north, double south, double east, double west);
        public Task RefreshAirQualityData();
    }
}
