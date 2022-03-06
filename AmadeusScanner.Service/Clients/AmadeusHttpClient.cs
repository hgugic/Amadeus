using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AmadeusScanner.Model.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AmadeusScanner.Service.Clients
{
    public class AmadeusHttpClient
    {
        private static readonly SemaphoreSlim AccessTokenSemaphore;
        private static AccessToken accessToken;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string oauthUrl;
        private readonly HttpClient httpClient;


        static AmadeusHttpClient()
        {
            accessToken = null!;
            AccessTokenSemaphore = new SemaphoreSlim(1, 1);
        }

        public AmadeusHttpClient(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            clientId = settings.Value.AmadeusApi;
            clientSecret = settings.Value.AmadeusId;
            oauthUrl = settings.Value.AmadeusAuthorizationApi;
        }

        public async Task<AccessToken> GetAccessToken()
        {
            if (accessToken is { Expired: false })
            {
                return accessToken;
            }

            accessToken = await FetchToken();
            return accessToken;
        }

        public HttpClient GetClient()
        {
            return httpClient;
        }

        private async Task<AccessToken> FetchToken()
        {
            try
            {
                await AccessTokenSemaphore.WaitAsync();

                if (accessToken is { Expired: false })
                {
                    return accessToken;
                }

                var request = new HttpRequestMessage(HttpMethod.Post, oauthUrl)
                {
                    Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("client_id", clientId),
                        new KeyValuePair<string, string>("client_secret", clientSecret),
                        new KeyValuePair<string, string>("scope", ""),
                        new KeyValuePair<string, string>("grant_type", "client_credentials")
                    })
                };

                using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                await using var responseContentStream = await response.Content.ReadAsStreamAsync();

                using StreamReader sr = new StreamReader(responseContentStream);
                using JsonReader reader = new JsonTextReader(sr);
                JsonSerializer serializer = new JsonSerializer();
                var token = serializer.Deserialize<AccessToken>(reader);
                return token;
            }
            finally
            {
                AccessTokenSemaphore.Release(1);
            }
        }
    }
}
