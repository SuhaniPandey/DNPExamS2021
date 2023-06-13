using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Teams
{
    [Key]
    public string  TeamName { get; set; }
    [Required,MaxLength(50)]
    public string  NameOfCoach { get; set; }
    public  int Ranking { get; set; }

    public ICollection<Players>? PlayersCollection { get; set; }

    public Teams(string teamName, string nameOfCoach, int ranking)
    {
        TeamName = teamName;
        NameOfCoach = nameOfCoach;
        Ranking = ranking;
    }
}