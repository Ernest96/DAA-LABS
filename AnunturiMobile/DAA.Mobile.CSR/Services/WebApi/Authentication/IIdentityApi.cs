using Refit;
using Anunturi.Mobile.Services.WebApi.Authentication.Dtos;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Authentication
{
    public interface IIdentityApi
    {
        [Headers("Content-Type:application/x-www-form-urlencoded")]
        [Post("/Token")]
        Task<LoginResponseDto> GetAccessToken([Body(BodySerializationMethod.UrlEncoded)]LoginRequestDto request);
    }
}
