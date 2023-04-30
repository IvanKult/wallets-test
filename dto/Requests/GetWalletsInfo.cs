using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;

namespace dto;

public static class GetWalletsInfo
{
    public class Request : IRequest<Response>, IPaged
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("page")]
        public uint Page { get; set; }
        [JsonProperty("pageSize")]
        public uint PageSize { get; set; }
    }
    public class Response
    {
        [JsonProperty("data")]
        public List<WalletInfo> Data { get; set; }
    }
}