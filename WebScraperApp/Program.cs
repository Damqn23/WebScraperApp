using HtmlAgilityPack;
using WebScraperApp;

using var client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");

var iphoneUrl = "https://ardes.bg/smartfoni/smartfoni/apple";
List<Product> products = new();
try 
{
    string iphoneHtmlContent = await client.GetStringAsync(iphoneUrl);
    var iphoneDocument = new HtmlDocument();
    iphoneDocument.LoadHtml(iphoneHtmlContent);
    var mainDivs = iphoneDocument.DocumentNode.SelectNodes("//div[contains(@class, 'product') and contains(@class, 'energy-specs-prod')]");    if (mainDivs != null)
    {
        foreach (var product in mainDivs)
        {
            var nameDiv = product.SelectSingleNode(".//div[contains(@class, 'isTruncated')]/span");
            var priceDiv = product.SelectSingleNode(".//div[contains(@class, 'eur-price')]");            
            if (nameDiv != null && priceDiv != null)
            {
                string cleanName = HelperMethods.CleanText(nameDiv.InnerText);
                string cleanPrice = HelperMethods.CleanText(priceDiv.InnerText);
                double cleanPriceNoCurrency = HelperMethods.ExtractPrice(cleanPrice);
                Product iPhone = new Product(cleanName, cleanPriceNoCurrency);
                products.Add(iPhone);
            }
            else
            {
                if (nameDiv == null) Console.WriteLine("DEBUG: Name not found in a container.");
                if (priceDiv == null) Console.WriteLine("DEBUG: Price not found in a container.");
            }
        }
        
    }

    if (products.Count == 0)
    {
        Console.WriteLine($"No products found.");
    }
    else
    {
        Console.WriteLine($"\nConnecting to database to save {products.Count} records...");
        using var dbContext = new ScraperDbContext();
        await dbContext.AddRangeAsync(products);
        await dbContext.SaveChangesAsync();
        Console.WriteLine("Records saved successfully.\n");
    }
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Could not access Ardes.bg: {e.Message}");
}

