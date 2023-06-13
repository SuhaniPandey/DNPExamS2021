using WebApi.Models;

namespace BlazorApplication.HttpClient.IClientInterface;

public interface IPlayerService
{
    Task CreateAsync(Players playerCreation, string teamsname);
}