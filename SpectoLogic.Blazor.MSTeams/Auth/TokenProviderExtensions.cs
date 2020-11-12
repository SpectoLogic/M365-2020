using Microsoft.Extensions.DependencyInjection;

namespace SpectoLogic.Blazor.MSTeams.Auth
{
    public static class TokenProviderExtensions
    {
        public static IServiceCollection AddTokenProvider(this IServiceCollection services)
        => services.AddScoped<ITokenProvider, TokenProvider>()
        .AddHttpClient();
    }
}
