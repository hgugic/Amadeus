using Newtonsoft.Json;
using System;

namespace AmadeusScanner.Service.Clients
{
    public class AccessToken
    {

        private static readonly TimeSpan Threshold = new TimeSpan(0,2,0);

        private static readonly DateTime Time = DateTime.UtcNow;

        [JsonProperty("access_token")]
        public string Token { get; set; }


        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public bool Expired => Time + TimeSpan.FromSeconds(ExpiresIn) > DateTime.UtcNow + TimeSpan.FromSeconds(ExpiresIn) - Threshold;
    }
}
