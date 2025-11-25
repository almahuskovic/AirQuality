using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseIServices
{
    public interface IBaseCRUDService<T, TSearch, TInsert, TUpdate> : IBaseReadService<T, TSearch>
      where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        Task<T> Insert(TInsert request);
        Task<T> Update(Guid id, TUpdate request);
        Task<T> Delete(Guid id, bool hardDelete = false);
    }
}
