using Models.Dto;

namespace Infrastructure.Interfaces
{
    public interface IAirQualityService
    {
        public Task<AirQualityMeasurementDto?> GetLatestByCityId(int cityId);
        public Task<List<AirQualityApiResponse>?> GetAQIByCities(string country);
        public Task<List<AirQualityApiResponse>?> GetAQIForVisibleCities(double north, double south, double east, double west);
        public Task RefreshAirQualityData();
    }
}
