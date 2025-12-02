using AutoMapper;
using Infrastructure.BaseServices;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Models.Entities;
using Models.Requests.UserFavouriteCities;

namespace Infrastructure.Services
{
    public class FavouriteCitiesService : BaseCRUDService<FavouriteCitiesDto, FavouriteCities, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>, IFavouriteCitiesService
    {
        private readonly Context _context;
        private readonly IAirQualityService _airQuality;
        private readonly IUserContextService _userContextService;
        public FavouriteCitiesService(Context context, IMapper mapper, IAirQualityService airQuality, IUserContextService userContextService) : base(context, mapper)
        {
            _context = context;
            _airQuality = airQuality;
            _userContextService = userContextService;
        }

        public async Task<List<FavouriteCitiesDto>?> GetFavouriteCities(FavouriteCitiesSearchRequest request)
        {
            var query = _context.UserFavouriteCities.Include(x => x.City).AsQueryable();
            var userId = await _userContextService.GetCurrentUserIdAsync();
            query = query.Where(x => x.UserId == userId && x.IsDeleted == false);

            if (request.CityId != null)
            {
                query = query.Where(x => x.CityId.ToString() == request.CityId);
            }
            var result = new List<FavouriteCitiesDto>();
            foreach (var x in query)
            {
                var city = await _airQuality.GetLatestByCityId(x.CityId);
                result.Add(new FavouriteCitiesDto() { Id = x.Id, CityData = city, UserId = userId, CityId = city.CityId });
            }

            return result;
        }
        public override async Task<FavouriteCitiesDto> Insert(FavouriteCitiesUpsertRequest request)
        {
            var userId = await _userContextService.GetCurrentUserIdAsync();
            var exists = _context.UserFavouriteCities
                .Where(x => x.CityId == request.CityId && x.UserId == userId && !x.IsDeleted)
                .FirstOrDefault();

            if (exists != null)
            {
                throw new Exception("City is already in favourites.");
            }

            var favouriteCity = new FavouriteCities()
            {
                CityId = request.CityId,
                UserId = userId
            };
            _context.UserFavouriteCities.Add(favouriteCity);
            await _context.SaveChangesAsync();

            return _mapper.Map<FavouriteCitiesDto>(favouriteCity);
        }
    }
}
