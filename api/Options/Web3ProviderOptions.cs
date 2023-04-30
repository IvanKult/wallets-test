using Newtonsoft.Json;

namespace api.Options
{
    public class Web3ProviderOptions
    {
        [JsonProperty("Url")]
        public string EndpointUrl { get; set; }
        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }
    }
}