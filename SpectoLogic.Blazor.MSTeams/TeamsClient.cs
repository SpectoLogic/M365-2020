using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace SpectoLogic.Blazor.MSTeams
{
    public class TeamsClient : ITeamsClient
    {
        public TeamsClient(IJSRuntime jSRuntime)
        {
            _JSRuntime = jSRuntime;
        }
        private readonly IJSRuntime _JSRuntime;

        public ValueTask<string> GetUPN() => _JSRuntime.InvokeAsync<string>(MethodNames.GET_UPN_METHOD);
        public ValueTask<string> GetClientToken() => _JSRuntime.InvokeAsync<string>(MethodNames.GET_CLIENT_TOKEN_METHOD);

        public ValueTask<string> GetServerToken(string clientToken, string[] scopes)
        => _JSRuntime.InvokeAsync<string>(MethodNames.GET_SERVER_TOKEN_METHOD, clientToken, scopes);

        private class MethodNames
        {
            public const string GET_UPN_METHOD = "BlazorExtensions.SpectoLogicMSTeams.GetUPN";
            public const string GET_CLIENT_TOKEN_METHOD = "BlazorExtensions.SpectoLogicMSTeams.GetClientToken";
            public const string GET_SERVER_TOKEN_METHOD = "BlazorExtensions.SpectoLogicMSTeams.GetServerToken";
        }
    }
}
