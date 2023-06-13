using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.EFCDataAccess;

public class DataContext: DbContext
{
    public DbSet<Players>  Players { get; set; }
    public DbSet<Teams> Teams { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = Data.db");
    }
}