using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace AnunturiDesktop.Helpers
{
    public class HttpClientFactory
    {
        private static HttpClient _httpClient;

        public static HttpClient Create(string baseAddress)
        {
            if (_httpClient == null)
            {
                WebRequestHandler handler = new WebRequestHandler();
                ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

                _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseAddress),
                    Timeout = TimeSpan.FromSeconds(30),
                };

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("api-key", AppEnvironment.ApiKey);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            }

            return _httpClient;
        }

        public static void SetAuthenticationHeader(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
