using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseIServices
{
    public interface IBaseReadService<T, TSearch> where T : class where TSearch : class
    {
        public IEnumerable<T> Get(TSearch search = null);
        public T GetById(Guid id);
    }
}
