﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnunturiDesktop.Helpers
{
    public class BrokerHttpClientFactory
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
    }
}
