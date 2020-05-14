using AnunturiDesktop.DTO;
using Refit;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services.Auth
{
    public interface IAnuntApi
    {
        [Post("/api/Anunt/Publish")]
        Task<object> PublishAnunt(AnuntDto anuntDto);
    }
}
