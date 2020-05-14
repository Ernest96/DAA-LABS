using Anunturi.Mobile.AppStart;
using Anunturi.Mobile.Services.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILoggingService _loggingService;

        public HttpLoggingHandler(HttpMessageHandler innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            _loggingService = AutofacConfig.Resolve<ILoggingService>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _loggingService.Debug("Request:");
            _loggingService.Debug(request.ToString());
            if (request.Content != null)
            {
                _loggingService.Debug(await request.Content.ReadAsStringAsync());
            }
            _loggingService.Debug("\n");

            //request executing
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            _loggingService.Debug("Response:");
            _loggingService.Debug(response.ToString());
            if (response.Content != null)
            {
                _loggingService.Debug(await response.Content.ReadAsStringAsync());
            }
            _loggingService.Debug("\n");

            return response;
        }
    }
}
