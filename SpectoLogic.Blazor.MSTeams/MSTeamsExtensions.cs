using Microsoft.Extensions.DependencyInjection;

namespace SpectoLogic.Blazor.MSTeams{
    public static class MSTeamsExtensions
    {
        public static IServiceCollection AddMSTeams(this IServiceCollection services)
        => services.AddScoped<ITeamsClient, TeamsClient>();
    }}
