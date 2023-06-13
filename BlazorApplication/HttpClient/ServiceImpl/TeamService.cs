using System.Text.Json;
using BlazorApplication.HttpClient.IClientInterface;
using WebApi.Models;

namespace BlazorApplication.HttpClient.ServiceImpl;

public class TeamService:ITeamService
{

    private readonly System.Net.Http.HttpClient client;

    public TeamService(System.Net.Http.HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(Teams createTeams)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Teams", createTeams);
        if (!response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task<ICollection<Teams>> GetAllTeamAsync(string? teamName, int? ranking)
    {
        string query = ConstructQuery(teamName,ranking);

        HttpResponseMessage response = await client.GetAsync("/Teams"+query);
        string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Teams> teams = JsonSerializer.Deserialize<ICollection<Teams>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return teams;
    }

    public async Task<IEnumerable<Teams>?> getAllChildren()
    {
        HttpResponseMessage responseMessage = await client.GetAsync("/Teams");
        string result = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        IEnumerable<Teams> enumerable = JsonSerializer.Deserialize<IEnumerable<Teams>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return enumerable;
    }

    private static string ConstructQuery(string? teamName, int? ranking)
    {//check all the filter argument and check if they are null, in that case they should be ignored otherwise include the needed filter
        string query = "";
        if (!string.IsNullOrEmpty(teamName))
        {
            //query must always start with "?" and separated with "&"
            query += $"?teamName={teamName}";
        }
        
        if (ranking != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"ranking={ranking}";
        }
        return query;
    }
}