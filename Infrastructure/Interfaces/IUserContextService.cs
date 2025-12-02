using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserContextService
    {
        Task<string> GetCurrentUserIdAsync();
        Task<ApplicationUser> GetCurrentUserAsync();
    }
}
