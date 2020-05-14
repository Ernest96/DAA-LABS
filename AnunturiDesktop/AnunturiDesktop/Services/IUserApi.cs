using AnunturiDesktop.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services
{
    public interface IUserApi
    {
        [Get("/api/Users/GetUsers")]
        Task<IList<User>> GetUsers();

        [Post("/api/Users/Delete")]
        Task<IList<User>> Delete(string userId);
    }
}
