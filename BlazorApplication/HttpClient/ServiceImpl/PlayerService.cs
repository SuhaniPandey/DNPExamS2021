using BlazorApplication.HttpClient.IClientInterface;
using WebApi.Models;

namespace BlazorApplication.HttpClient.ServiceImpl;

public class PlayerService:IPlayerService
{

    private readonly System.Net.Http.HttpClient client;

    public PlayerService(System.Net.Http.HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(Players playerCreation, string teamsname)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync($"/Players/{teamsname}", playerCreation);
        if (!response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }
}