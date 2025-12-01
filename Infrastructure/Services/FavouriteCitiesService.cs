using AutoMapper;
using CsvHelper;
using Infrastructure.BaseServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Models.Entities;
using Models.IServices;
using Models.Requests.UserFavouriteCities;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class FavouriteCitiesService : BaseCRUDService<FavouriteCitiesDto, FavouriteCities, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>, IFavouriteCities
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAirQuality _airQuality;
        public FavouriteCitiesService(Context context, IMapper mapper, IHttpContextAccessor contextAccessor, IAirQuality airQuality) : base(context, mapper)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _airQuality = airQuality;
        }
        public async Task<FavouriteCitiesDto?> GetFavouriteCities()
        {
            var query = _context.UserFavouriteCities.Include(x=>x.City).AsQueryable();
            var userId = GetCurrentUserId();
            query = query.Where(x => x.UserId == userId);
            
            var cities = new List<AirQualityMeasurementDto>();
            foreach (var x in query)
            {
                var city = await _airQuality.GetLatestByCityId(x.CityId);
                cities.Add(city);
            }
            var result = new FavouriteCitiesDto()
            {
                UserId = userId,
                Cities = cities
            };

            return result;
        }
        public override async Task<FavouriteCitiesDto> Insert(FavouriteCitiesUpsertRequest request)
        {
            var favouriteCity = new FavouriteCities()
            {
                CityId = request.CityId,
                UserId = GetCurrentUserId()
            };
            _context.UserFavouriteCities.Add(favouriteCity);
            await _context.SaveChangesAsync();
           
            return _mapper.Map<FavouriteCitiesDto>(favouriteCity);
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
                ?.Value;
        }
    }
}
