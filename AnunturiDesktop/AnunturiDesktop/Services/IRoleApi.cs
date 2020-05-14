using AnunturiDesktop.DTO;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services
{
    public interface IRoleApi
    {
        [Get("/api/Users/GetRoles")]
        Task<IEnumerable<RoleDto>> GetRoles();
    }
}
