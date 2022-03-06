using AmadeusScanner.Common.Url;
using AmadeusScanner.Model;
using AmadeusScanner.Model.Amadeus;
using AmadeusScanner.Model.Settings;
using AmadeusScanner.Service.Clients;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Amadeus
{
    public class AmadeusList
    {
        public class Query : IRequest<AmadeusFlightOffers> 
        {
            public AmadeusItinerary Itinerary { get; set; }
        }

        public class Handler : IRequestHandler<Query, AmadeusFlightOffers>
        {
            private readonly AmadeusHttpClient client;
            private readonly string baseUrl;

            public Handler(AmadeusHttpClient client, IOptions<AppSettings> appSettings)
            {
                this.client = client;
                baseUrl = appSettings.Value.AmadeusBaseUrl;
            }

            public async Task<AmadeusFlightOffers> Handle(Query request, CancellationToken cancellationToken)
            {

                var accessToken = await client.GetAccessToken();
                var query = UrlGenerator<AmadeusItinerary>.GenerateUrl(request.Itinerary, baseUrl);
                var req = new HttpRequestMessage(HttpMethod.Get, query);
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);

                using var response = await client.GetClient().SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                await using var responseContentStream = await response.Content.ReadAsStreamAsync();

                using StreamReader sr = new StreamReader(responseContentStream);
                using JsonReader reader = new JsonTextReader(sr);
                JsonSerializer serializer = new JsonSerializer();
                var amadeusflight = serializer.Deserialize<AmadeusFlightOffers>(reader);

                return amadeusflight;
            }
        }
    }
}
