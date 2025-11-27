using Models.BaseIServices;
using Models.Dto;
using Models.Requests.Cities;
using Models.Requests.UserFavouriteCities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.IServices
{
    public interface IUserFavouriteCities : IBaseCRUDService<FavouriteCitiesDto, FavouriteCitiesSearchRequest, FavouriteCitiesUpsertRequest, FavouriteCitiesUpsertRequest>
    {
    }
}
