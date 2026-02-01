namespace WebScraperApp.Services;

public interface IScraperService
{
    Task<List<Product>> ScrapeProductsAsync();
}