using AutoMapper;
using Models.Dto;
using Models.Entities;
using Models.IServices;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class AirQualityService : IAirQuality
    {
        private readonly HttpClient _httpClient;
        private readonly ICity _httpCity;
        private readonly Context _context;
        private readonly IMapper _mapper;

        public AirQualityService(Context context, IHttpClientFactory factory, ICity httpCity,IMapper mapper)
        {
            _httpClient = factory.CreateClient("AirQualityAPI");
            _httpCity = httpCity;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AirQualityMeasurementDto?> GetLatestByCityId(int cityId)
        {
            var city = _httpCity.GetById(cityId);
            if (city == null)
            {
                return null;
            }
            var query = _context.AirQualityMeasurements.AsEnumerable();
            query = query.Where(x => cityId == x.CityId);

            var result = query.Select(x => new AirQualityMeasurementDto()
            {
                AQI = x.AQI,
                MeasuredAt = x.MeasuredAt,
                CityId = x.CityId,
                City = x.City.Name,
                Country = x.City.Country,
            }).FirstOrDefault();

            return result;
        }

        public async Task<List<AirQualityApiResponse>?> GetAQIByCities(string iso)
        {
            var cities = _httpCity.Get(new Models.Requests.Cities.CitySearchRequest() { ISO = iso, PageSize = null });
            if (cities == null || !cities.Any())
            {
                return null;
            }
            List<AirQualityApiResponse> result = new List<AirQualityApiResponse>();
            try
            {
                var query = _context.AirQualityMeasurements.AsEnumerable();
                query = query.Where(x => cities.Any(y => y.Id == x.CityId));

                result = query.Select(x => new AirQualityApiResponse()
                {
                    Longitude = x.City.Longitude,
                    Latitude = x.City.Latitude,
                    Current = new Current() { Us_Aqi = x.AQI }
                }).ToList();
            }
            catch (Exception e)
            {

                throw;
            }

            return result;
        }

        public async Task<List<AirQualityApiResponse>?> GetAQIForVisibleCities(double north, double south, double east, double west)
        {
            var cities = _httpCity.Get(new Models.Requests.Cities.CitySearchRequest() { North = north, East = east, West = west, South = south, PageSize = 50 });
            if (cities == null || !cities.Any())
            {
                return null;
            }

            var query = _context.AirQualityMeasurements.AsEnumerable();
            query = query.Where(x => cities.Any(y => y.Id == x.CityId));

            var result = query.Select(x => new AirQualityApiResponse()
            {
                Longitude = x.City.Longitude,
                Latitude = x.City.Latitude,
                Current = new Current() { Us_Aqi = x.AQI }
            }).ToList();
           
            return result;
        }

        public async Task RefreshAirQualityData()
        {
            var cities = _context.Cities.AsQueryable();
            var airQualityMeasurements = _context.AirQualityMeasurements.AsQueryable();
            foreach (var city in cities)
            {
                try
                {
                    var airQuality = airQualityMeasurements.Where(x => x.CityId == city.Id).FirstOrDefault();
                    var minutes = DateTime.Now.Minute - airQuality?.MeasuredAt.Minute;
                    if (airQuality == null || minutes > 60)
                    {
                        Console.WriteLine($"{_httpClient.BaseAddress}?latitude={city.Latitude}&longitude={city.Longitude}&current=us_aqi&timezone=UTC");
                        var response = await _httpClient.GetStringAsync($"{_httpClient.BaseAddress}?latitude={city.Latitude}&longitude={city.Longitude}&hourly=pm2_5&current=us_aqi&timezone=UTC");
                        if (response != null)
                        {
                            var data = JsonConvert.DeserializeObject<AirQualityApiResponse>(response);
                            var cache = _context.AirQualityMeasurements.FirstOrDefault(x => x.CityId == city.Id);
                            if (cache == null)
                            {
                                cache = new AirQualityMeasurement { CityId = city.Id };
                                _context.AirQualityMeasurements.Add(cache);
                            }

                            cache.AQI = data.Current.Us_Aqi;
                            cache.MeasuredAt = DateTime.UtcNow;

                            await _context.SaveChangesAsync();
                        }

                        await Task.Delay(1000); // delay 1 sec to avoid rate limit
                    }
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error fetching AQI for city " + city.Name);
                }
            }
        }

    }
}
