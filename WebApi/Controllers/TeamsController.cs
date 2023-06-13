using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.EFCDataAccess;
using WebApi.Models;

namespace WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class TeamsController: ControllerBase
{
    private readonly DataContext context;

    public TeamsController(DataContext context)
    {
        this.context = context;
    }
    
    [HttpPost]
    public async Task<ActionResult<Teams>> CreateAsync(Teams teams)
    {
        try
        {
            EntityEntry<Teams> toCreate = await context.Teams.AddAsync(teams);
            Teams added = toCreate.Entity;
            await context.SaveChangesAsync();
            return Created($"/Teams/{added.TeamName}", teams);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Teams>>> GetAllTeamAsync([FromQuery] string? teamName,
        [FromQuery] int? ranking)
    {
        try
        {
            IQueryable<Teams> teams = context.Teams.Include(teams1 =>teams1.PlayersCollection);
            if (!string.IsNullOrEmpty(teamName))
            {
                teams = teams.Where(a => a.TeamName.Contains(teamName));
            }

            if (ranking != null)
            {
                teams = teams.Where(team => team.Ranking <= ranking);
            }

            List<Teams> teamsList = teams.ToList();
            return Ok(teamsList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}