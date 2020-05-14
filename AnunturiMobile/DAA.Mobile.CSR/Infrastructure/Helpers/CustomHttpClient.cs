using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Anunturi.Mobile.Infrastructure.Helpers
{
    class CustomHttpClient : DefaultHttpClient
    {
        private readonly System.Net.Security.RemoteCertificateValidationCallback _serverCertificateValidationCallback;

        public CustomHttpClient(System.Net.Security.RemoteCertificateValidationCallback serverCertificateValidationCallback) : base()
        {
            this._serverCertificateValidationCallback = serverCertificateValidationCallback;
        }

        protected override HttpMessageHandler CreateHandler()
        {
            var handler = base.CreateHandler() as HttpClientHandler;

            handler.ServerCertificateCustomValidationCallback += (sender, cert, chaun, ssPolicyError) =>
            {
                return true;
            };

            return handler;
        }
    }
}
