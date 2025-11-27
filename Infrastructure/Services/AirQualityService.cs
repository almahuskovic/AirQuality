using Models.Dto;
using Models.IServices;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class AirQualityService : IAirQuality
    {
        private readonly HttpClient _httpClient;
        private readonly ICity _httpCity;

        public AirQualityService(IHttpClientFactory factory, ICity httpCity)
        {
            _httpClient = factory.CreateClient("AirQualityAPI");
            _httpCity = httpCity;
        }

        public async Task<AirQualityMeasurementDto?> GetLatestByCityId(int cityId)
        {
            //var city = _httpCity.Get(new Models.Requests.Cities.CitySearchRequest() { Name = cityName }).FirstOrDefault();
            var city = _httpCity.GetById(cityId);
            if (city == null)
            {
                return null;
            }
           
            var response = await _httpClient.GetStringAsync($"{_httpClient.BaseAddress}?latitude={city.Latitude}&longitude={city.Longitude}&hourly=pm10,pm2_5,ozone,nitrogen_dioxide,sulphur_dioxide,carbon_monoxide,ammonia&timezone=UTC");
            if (response == null) { return null; }
            var data=JsonConvert.DeserializeObject<AirQualityMeasurementDto>(response);


            return data;
        }
    }
}
