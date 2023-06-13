using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.EFCDataAccess;
using WebApi.Models;

namespace WebApi.Controllers;


[ApiController]
[Route("[controller]")]
public class PlayersController: ControllerBase
{

    private readonly DataContext context;

    public PlayersController(DataContext context)
    {
        this.context = context;
    }

    [HttpPost("{teamName}")]
    public async Task<ActionResult<Players>> CreateAsync([FromRoute] string teamName, [FromBody] Players players)
    {
        try
        {
            Teams? teams= await context.Teams.Include(teams1 =>teams1.PlayersCollection).FirstOrDefaultAsync(teams1 =>teams1.TeamName == teamName);
            if (teams==null) {
                return StatusCode(500, "teams doesnt exist");
            }
            teams.PlayersCollection.Add(players);                  // this adds book to appropraite author
            EntityEntry<Players> entry = await context.Players.AddAsync(players);
            await context.SaveChangesAsync();
            Players players1 = entry.Entity;
            return Created($"/Players/{players.Id}", players);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete ("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            Players? existing = await context.Players.FindAsync(id);
            if (existing == null)
            {
                throw new Exception($"Players with id {id} not found");
            }

            context.Players.Remove(existing);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}