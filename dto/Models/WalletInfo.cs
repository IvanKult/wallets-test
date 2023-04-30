using Newtonsoft.Json;

namespace dto;
public class WalletInfo
{
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("address")]
    public string Address { get; set; }
    [JsonProperty("balance")]
    public decimal Balance { get; set; }
}
