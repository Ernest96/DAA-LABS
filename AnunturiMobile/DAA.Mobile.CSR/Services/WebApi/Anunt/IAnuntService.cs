using Anunturi.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Anunt
{
    public interface IAnuntService
    {
        Task<IList<PushInfo>> GetPushInfos();
        Task<PushModel> GetPush(int id);
    }
}
