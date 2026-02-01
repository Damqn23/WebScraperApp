using WebScraperApp.Services;

namespace WebScraperApp;

public class PostgresDatabaseService: IDatabaseService
{
    private readonly ScraperDbContext _dbContext;
    
    public PostgresDatabaseService(ScraperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task SaveProductsAsync(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No products to save.");
            return;
        }

        Console.WriteLine($"Saving {products.Count} products to database...");
        
        _dbContext.Products.AddRange(products);
        
        await _dbContext.SaveChangesAsync();
        
        Console.WriteLine("Done!");
    }
}