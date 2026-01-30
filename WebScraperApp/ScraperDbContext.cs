using Microsoft.EntityFrameworkCore;

namespace WebScraperApp;

public class ScraperDbContext: DbContext
{
    public DbSet<Product> Products { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=ScraperDb;Username=damqndimov;Password=");
        
    }
}