using Microsoft.Extensions.DependencyInjection;
using WebScraperApp;
using WebScraperApp.Services;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<ScraperDbContext>(); 
serviceCollection.AddHttpClient<IScraperService, ArdesScraperService>(); 
serviceCollection.AddTransient<IDatabaseService, PostgresDatabaseService>(); 

var serviceProvider = serviceCollection.BuildServiceProvider();

try
{
    var scraper = serviceProvider.GetRequiredService<IScraperService>();
    var dbService = serviceProvider.GetRequiredService<IDatabaseService>();

    List<Product> products = await scraper.ScrapeProductsAsync();

    if (products.Count > 0)
    {
        await dbService.SaveProductsAsync(products);
    }
    else
    {
        Console.WriteLine("No products found.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Critical Error: {ex.Message}");
}