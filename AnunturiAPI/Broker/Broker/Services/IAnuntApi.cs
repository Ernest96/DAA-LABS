using BLL.DTO;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Broker.Services
{
    public interface IAnuntApi
    {
        [Post("/api/Anunt/PublishMany")]
        Task<object> PublishAnunt(IList<AnuntDto> anunturi);
    }
}
