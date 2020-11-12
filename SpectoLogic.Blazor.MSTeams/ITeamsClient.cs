using System.Threading.Tasks;

namespace SpectoLogic.Blazor.MSTeams{
    public interface ITeamsClient
    {
        ValueTask<string> GetClientToken();
        ValueTask<string> GetUPN();
    }}
