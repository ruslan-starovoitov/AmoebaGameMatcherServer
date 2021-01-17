using Newtonsoft.Json;

namespace Services.Services.GoogleApi
{
    public class GoogleResponse
    {
        [JsonProperty("obfuscatedExternalAccountId")]
        public string ObfuscatedExternalAccountId;
    }
}