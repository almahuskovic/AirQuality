using AutoMapper;
using Infrastructure.BaseServices;
using Models.Dto;
using Models.Entities;
using Models.IServices;
using Models.Requests.Cities;
using System.Globalization;

namespace Infrastructure.Services
{
    public class CityService : BaseCRUDService<CityDto, City, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>, ICity
    {
        public CityService(Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IEnumerable<CityDto> Get(CitySearchRequest search)
        {
            var entity = Context.Set<City>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                entity = entity.Where(x => x.Name.Contains(search.Name));
            }

            if (!string.IsNullOrWhiteSpace(search.ISO))
            {
                entity = entity.Where(x => x.ISO.Contains(search.ISO));
            }

            if (search.South.HasValue && search.West.HasValue && search.East.HasValue && search.North.HasValue)
            {
                entity = entity.Where(x => x.Latitude <= search.North && x.Latitude >= search.South &&
                        x.Longitude <= search.East && x.Longitude >= search.West);
            }

            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                var skip = (search.Page.Value - 1) * search.PageSize.Value;
                entity = entity.Skip(skip).Take(search.PageSize.Value);
            }

            var list = entity.ToList();
            return _mapper.Map<List<CityDto>>(list);
        }

        public async Task ImportCitiesInDB()
        {
            if (Context.Cities.Any())
            {
                Console.WriteLine("Data already exists");
                return;
            }

            var cities = new List<City>();

            using (var reader = new StreamReader("./wwwroot/worldcities.csv"))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var city = new City
                    {
                        Name = values[0].Trim('"'),
                        Country = values[4].Trim('"'),
                        Latitude = double.Parse(values[2].Trim('"'), CultureInfo.InvariantCulture),
                        Longitude = double.Parse(values[3].Trim('"'), CultureInfo.InvariantCulture),
                        ISO = values[5].Trim('"'),
                    };

                    cities.Add(city);
                }
            }

            Context.Cities.AddRange(cities);
            Context.SaveChanges();
            Console.WriteLine("Success.");
        }
    }
}
