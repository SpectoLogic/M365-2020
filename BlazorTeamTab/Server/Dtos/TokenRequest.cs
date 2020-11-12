using System.Text.Json.Serialization;

namespace SpectoLogic.Blazor.MSTeams.Auth
{
    public class TokenRequest
    {
        [JsonPropertyName("tid")]
        public string Tid { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("scopes")]
        public string[] Scopes { get; set; }
    }
}
