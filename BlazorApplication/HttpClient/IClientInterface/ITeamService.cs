using WebApi.Models;

namespace BlazorApplication.HttpClient.IClientInterface;

public interface ITeamService
{
    Task CreateAsync(Teams createTeams);

    Task<ICollection<Teams>> GetAllTeamAsync(string? teamName,
        int? ranking);
    
    Task<IEnumerable<Teams>?> getAllChildren();
}