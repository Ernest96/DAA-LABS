using Anunturi.Mobile.Infrastructure.Constants;
using ModernHttpClient;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    public static class HttpClientFactory
    {
        // _httpClient is static in order to reuse it for all the services
        private static HttpClient _httpClient;
        public static HttpClient Create(string baseAddress)
        {
            if (_httpClient == null)
            {
                HttpClientHandler handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback += (sender, cert, chaun, ssPolicyError) =>
                {
                    return true;
                };

                ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseAddress),
                    Timeout = TimeSpan.FromSeconds(30),
                };

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("api-key", ApiConstants.ApiKey);
            }

            return _httpClient;
        }

        public static void SetAuthenticationHeader(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
