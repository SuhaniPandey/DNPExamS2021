using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Players
{
    [Key]
    public int  Id { get; set; }
    [Required,MaxLength(50)]
    public string  Name { get; set; }
    [Range(1,99)]
    public int   ShirtNumber { get; set; }
    public decimal Salary { get; set; }
    public int  GoalsThisSeason { get; set; }
    [Required]
    public string  Position { get; set; }

    public Players(string name, int shirtNumber, decimal salary, int goalsThisSeason, string position)
    {
        Name = name;
        ShirtNumber = shirtNumber;
        Salary = salary;
        GoalsThisSeason = goalsThisSeason;
        Position = position;
    }
}