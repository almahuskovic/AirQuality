using Models.Dto;
using Models.IServices;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class AirQualityService : IAirQuality
    {
        private readonly HttpClient _httpClient;
        private readonly ICity _httpCity;

        public AirQualityService(HttpClient httpClient, ICity httpCity)
        {
            _httpClient = httpClient;
            _httpCity = httpCity;
        }

        public async Task<AirQualityMeasurementDto?> GetLatestByCity(string cityName)
        {
            return null;
            //var city = _httpCity.Get(new Models.Requests.Cities.CitySearchRequest() { Name = cityName });
            //var response = await _httpClient.GetFromJsonAsync<AirQualityMeasurementDto>($"latest?city={city}");
            //if (response == null) return null;

           
            //return new AirQualityMeasurementDto
            //{
            //    City = city,
            //    PM25 = response.Results.FirstOrDefault()?.Measurements.FirstOrDefault(m => m.Parameter == "pm25")?.Value ?? 0,
            //    PM10 = response.Results.FirstOrDefault()?.Measurements.FirstOrDefault(m => m.Parameter == "pm10")?.Value ?? 0,
            //    MeasuredAt = DateTime.UtcNow
            //};
        }
    }
}
