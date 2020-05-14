using Anunturi.Mobile.AppStart;
using Anunturi.Mobile.Infrastructure.Constants;
using Anunturi.Mobile.Infrastructure.Helpers;
using Anunturi.Mobile.Services.Common.Settings;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class NativeHttpClientSignalR : IHttpClient
{
    private HttpClient _client;
    private IConnection _connection;

    public void Initialize(IConnection connection)
    {
        _connection = connection;
        HttpClientHandler handler = new HttpClientHandler();

        handler.ServerCertificateCustomValidationCallback += (sender, cert, chaun, ssPolicyError) =>
        {
            return true;
        };
        ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;

        _client = new HttpClient(handler);

        var settingsService = AutofacConfig.Resolve<ISettingsService>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settingsService.UserAccessToken);
        _client.DefaultRequestHeaders.Add("api-key", ApiConstants.ApiKey);
        _client.Timeout = TimeSpan.FromSeconds(10); //NOTE: infinite timeout because upper SignalR layer handles timeouts
    }

    public async Task<IResponse> Get(string url, Action<IRequest> prepareRequest, bool isLongRunning)
    {
        var request = new Request(HttpMethod.Get, url);
        prepareRequest(request);
        return await Send(request, isLongRunning);
    }

    public async Task<IResponse> Post(string url, Action<IRequest> prepareRequest, IDictionary<string, string> postData, bool isLongRunning)
    {
        var request = new Request(HttpMethod.Post, url);
        request.Content = new FormUrlEncodedContent(postData);
        prepareRequest(request);
        return await Send(request, isLongRunning);
    }

    private async Task<IResponse> Send(Request request, bool isLongRunning)
    {
        var option = isLongRunning ? HttpCompletionOption.ResponseHeadersRead : HttpCompletionOption.ResponseContentRead;
        var httpResponse = await _client.SendAsync(request, option, request.Cancellation.Token);
        var response = new Response(httpResponse);
        await response.ReadStream();
        return response;
    }

    private class Request : HttpRequestMessage, IRequest
    {
        public Request(HttpMethod method, string url) : base(method, url) { }

        public CancellationTokenSource Cancellation = new CancellationTokenSource();

        public string UserAgent { get; set; }

        public string Accept { get; set; }

        public void Abort()
        {
            Cancellation.Cancel();
        }

        public void SetRequestHeaders(IDictionary<string, string> headers)
        {
            if (UserAgent != null)
            {
                Headers.TryAddWithoutValidation("User-Agent", UserAgent);
            }
            if (Accept != null)
            {
                Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));
            }
            foreach (KeyValuePair<string, string> headerEntry in headers)
            {
                Headers.Add(headerEntry.Key, headerEntry.Value);
            }
        }
    }

    private class Response : IResponse
    {
        private HttpResponseMessage _response;
        private Stream _stream;

        public Response(HttpResponseMessage response)
        {
            _response = response;
        }

        public async Task ReadStream()
        {
            _stream = await _response.Content.ReadAsStreamAsync();
        }

        public Stream GetStream()
        {
            return _stream;
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
            if (_response != null)
            {
                _response.Dispose();
                _response = null;
            }
        }
    }
}