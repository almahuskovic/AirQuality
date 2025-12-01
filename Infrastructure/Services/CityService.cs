using AutoMapper;
using CsvHelper;
using Infrastructure.BaseServices;
using Models.Dto;
using Models.Entities;
using Models.IServices;
using Models.Requests.Cities;
using System.Collections.Generic;
using System.Formats.Asn1;
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
                entity = entity.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()) || x.Country.ToLower().Contains(search.Name.ToLower()));
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

        public void ImportCitiesInDB()
        {
            if (Context.Cities.Any())
            {
                Console.WriteLine("Data already exists");
                return;
            }

            var cities = new List<CityCsv>();

            using (var reader = new StreamReader("./wwwroot/worldcities.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    var records = csv.GetRecords<CityCsv>().ToList();
                    cities.AddRange(records);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            if(cities.Any())
            {
                var citiesToAdd = new List<CityCsv>();
                citiesToAdd.AddRange(cities.Where(x => x.capital == "primary" || x.iso2 == "BA").ToList());
                citiesToAdd.AddRange(cities.Where(x => x.capital == "admin" && x.population != null).OrderByDescending(x => x.population).Take(80));

                var citiesDb = citiesToAdd.Select(x => new City()
                {
                    Name = x.city,
                    Longitude = x.lng,
                    Latitude = x.lat,
                    Country = x.country,
                    IsCapital = x.capital == "primary" ? true : false,
                    ISO = x.iso2,
                }).ToList();

                Context.Cities.AddRange(citiesDb);
                Context.SaveChanges();
            }
           
            Console.WriteLine("Success.");
        }
    }
}
