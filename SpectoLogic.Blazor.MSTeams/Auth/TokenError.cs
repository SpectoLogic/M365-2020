using System.Text.Json.Serialization;

namespace SpectoLogic.Blazor.MSTeams.Auth
{
    public class TokenError
    {
        /// <summary>
        /// May include things like "invalid_grant"
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("error_description")]
        public string Description { get; set; }
        [JsonPropertyName("error_codes")]
        public int[] Codes { get; set; }
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        [JsonPropertyName("trace_id")]
        public string TraceId { get; set; }
        [JsonPropertyName("correlation_id")]
        public string CorrelationId { get; set; }
        [JsonPropertyName("error_uri")]
        public string ErrorUri { get; set; }
    }

}
