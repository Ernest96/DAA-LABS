using AnunturiDesktop.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services
{
    public class RoleService
    {
        private static List<string> _roles = new List<string>();

        private readonly IRoleApi _roleApi;

        public RoleService()
        {
            _roleApi = RestService.For<IRoleApi>(HttpClientFactory.Create(AppEnvironment.ApiBaseUrl));
        }

        public async Task<List<string>> GetRoles()
        {
            if (!_roles.Any())
            {
                var dtos = await _roleApi.GetRoles();
                _roles = dtos.Select(x => x.Name).OrderByDescending(x => x).ToList();
            }

            return _roles;
        }
    }
}
