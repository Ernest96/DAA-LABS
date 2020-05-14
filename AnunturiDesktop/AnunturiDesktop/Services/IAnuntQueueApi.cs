using AnunturiDesktop.DTO;
using Refit;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services
{
    public interface IAnuntQueueApi
    {
        [Post("/api/AnuntQueue/Enqueue")]
        Task<object> Enqueue(AnuntDto anunt);
    }
}
