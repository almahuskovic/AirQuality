using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BaseModels
{
    public class BaseSearchObject
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public BaseSearchObject()
        {
            Page = 1;
            PageSize = 10;
        }
    }
}
