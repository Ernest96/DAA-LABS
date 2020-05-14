using AnunturiDesktop.DTO;
using Refit;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services.Auth
{
    public interface IIdentityApi
    {
        [Headers("Content-Type:application/x-www-form-urlencoded")]
        [Post("/Token")]
        Task<LoginResponseDto> GetAccessToken([Body(BodySerializationMethod.UrlEncoded)]LoginRequestDto request);
    }
}
