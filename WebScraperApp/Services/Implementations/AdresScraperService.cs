using HtmlAgilityPack;
using WebScraperApp.Services;

namespace WebScraperApp;

public class ArdesScraperService: IScraperService
{
    private readonly HttpClient _client;
    private const string _iphoneUrl = "https://ardes.bg/smartfoni/smartfoni/apple";
    
    public ArdesScraperService(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
    }
    public async Task<List<Product>> ScrapeProductsAsync()
    {
        Console.WriteLine($"Scraping for {_iphoneUrl} started...");
        List<Product> products = new();
        try
        {
            string html = await _client.GetStringAsync(_iphoneUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var mainDivs = doc.DocumentNode.SelectNodes("//div[contains(@class, 'product') and contains(@class, 'energy-specs-prod')]");

            if (mainDivs == null) return products;

            foreach (var div in mainDivs)
            {
                var nameNode = div.SelectSingleNode(".//div[contains(@class, 'isTruncated')]/span");
                var priceNode = div.SelectSingleNode(".//div[contains(@class, 'eur-price')]");

                if (nameNode != null && priceNode != null)
                {
                    string name = HelperMethods.CleanText(nameNode.InnerText);
                    string rawPrice = HelperMethods.CleanText(priceNode.InnerText);
                    double price = HelperMethods.ExtractPrice(rawPrice);

                    products.Add(new Product(name, price));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scraping: {ex.Message}");
        }
        return products;
    }
}