using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnunturiDesktop.Services
{
    public interface ISmsApi
    {
        [Post("/api/Anunt/SendSms")]
        Task<IEnumerable<string>> SendSms(string text);
    }
}
