using Models.BaseIServices;
using Models.Dto;
using Models.Requests;
using Models.Requests.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.IServices
{
    public interface ICity : IBaseCRUDService<CityDto, CitySearchRequest, CityUpsertRequest, CityUpsertRequest>
    {
    }
}
