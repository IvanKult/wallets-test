using Newtonsoft.Json;

namespace site.Options;

public class ApiOptions
{
    [JsonProperty("Url")]
    public string Url { get; set; }
}