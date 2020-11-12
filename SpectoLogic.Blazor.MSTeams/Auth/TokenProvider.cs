using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpectoLogic.Blazor.MSTeams.Auth
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpClientFactory _clientFactory;

        public TokenProvider(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TokenResponse> GetToken(string tenantId, string token, string clientId, string clientSecret, string[] scopes)
        {
            string requestUrlString = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";

            Dictionary<string, string> values = new Dictionary<string, string>
                {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer" },
                { "assertion", token },
                { "requested_token_use", "on_behalf_of" },
                { "scope", string.Join(" ",scopes) }
                };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpClient client = _clientFactory.CreateClient();
            HttpResponseMessage resp = await client.PostAsync(requestUrlString, content);
            if (resp.IsSuccessStatusCode)
            {
                string respContent = await resp.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TokenResponse>(respContent);
            }
            else
            {
                string respContent = await resp.Content.ReadAsStringAsync();
                TokenError tokenError = JsonSerializer.Deserialize<TokenError>(respContent);
                throw new TokenErrorException(tokenError);
            }
        }
    }
}
