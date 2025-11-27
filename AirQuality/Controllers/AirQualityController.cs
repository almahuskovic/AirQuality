using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.IServices;

namespace AirQuality.Controllers
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

        [HttpGet("{id}")]
        public async Task<AirQualityMeasurementDto?> GetLatestByCity(int id)
        {
            return await _airService.GetLatestByCityId(id);
        }
    }
}
