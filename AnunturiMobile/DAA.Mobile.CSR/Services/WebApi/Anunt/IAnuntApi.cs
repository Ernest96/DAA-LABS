using Anunturi.Mobile.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.Anunt.WebApi
{
    public interface IAnuntApi
    {
        [Get("/api/anunt/GetAnunturi")]
        Task<IList<PushInfo>> GetAnunturi(string role);

        [Get("/api/anunt/GetAnunt")]
        Task<PushModel> GetAnunt(int pushId);
    }
}
