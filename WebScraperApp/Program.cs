using HtmlAgilityPack;
using WebScraperApp;

using var client = new HttpClient();

string url = "https://noseway.bg/"; 

string htmlContent = await client.GetStringAsync(url);

var document = new HtmlDocument();
document.LoadHtml(htmlContent);


var skipLinks = document.DocumentNode.SelectSingleNode("//div[@class='wd-skip-links']");
if (skipLinks != null)
{
    var links = skipLinks.SelectNodes(".//a");
    if (links != null)
    {
        foreach (var link in links)
        {
            string text = HelperMethods.CleanText(link.InnerText);
            string urlPath = link.GetAttributeValue("href", string.Empty);
            
            Console.WriteLine($"Text: {text}, URL: {urlPath}");
        }
    }
}


var mainHeader = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'whb-general-header')]");
if (mainHeader != null)
{
    var navLink = mainHeader.SelectSingleNode(".//a"); 
    
    var container = mainHeader.SelectSingleNode(".//div[@class='container']");
    var flexRow = mainHeader.SelectSingleNode(".//div[contains(@class, 'whb-flex-row')]");

    string cleanTextContainer = HelperMethods.CleanText(container?.InnerText);
    string cleanTextFlexRow = HelperMethods.CleanText(flexRow?.InnerText);
    
    //Console.WriteLine($"Cleaned: {cleanTextContainer}");
    //Console.WriteLine("0-------------------");
    //Console.WriteLine($"Cleaned: {cleanTextFlexRow}");
}
else
{
    Console.WriteLine("Main header not found.");
}