using System.Threading.Tasks;

namespace SpectoLogic.Blazor.MSTeams.Auth{
    public interface ITokenProvider
    {
        Task<TokenResponse> GetToken(string tenantId, string token, string clientId, string clientSecret, string[] scopes);
    }}
