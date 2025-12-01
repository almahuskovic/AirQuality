using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto
{
    public class FavouriteCitiesDto
    {
        public List<AirQualityMeasurementDto> Cities { get; set; }
        public int CityId { get; set; }
        public string UserId { get; set; }
    }
}
