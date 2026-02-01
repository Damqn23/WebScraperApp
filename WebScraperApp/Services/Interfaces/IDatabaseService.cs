namespace WebScraperApp.Services;

public interface IDatabaseService
{
    Task SaveProductsAsync(List<Product> products);
}