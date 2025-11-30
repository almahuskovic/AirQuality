using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.IServices;

namespace AirQuality.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirQualityController : ControllerBase
    {
        protected readonly IAirQuality _airService;

        public AirQualityController(IAirQuality airService)
        {
            _airService = airService;
        }

        [HttpGet("GetLatestByCity/{id}")]
        public async Task<AirQualityMeasurementDto?> GetLatestByCity(int id)
        {
            return await _airService.GetLatestByCityId(id);
        }

        [HttpGet]
        [Route("{action}")]
        public async Task<List<AirQualityApiResponse>?> GetAQIByCities([FromQuery] string iso)
        {
            return await _airService.GetAQIByCities(iso);
        }

        [HttpGet]
        [Route("{action}")]
        public async Task<List<AirQualityApiResponse>?> GetAQIForVisibleCities([FromQuery] double north, double south, double east, double west)
        {
            return await _airService.GetAQIForVisibleCities(north, south, east, west);
        }
    }
}
