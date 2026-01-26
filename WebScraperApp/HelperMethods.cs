namespace WebScraperApp;
using System.Text.RegularExpressions;
using System.Net;
public class HelperMethods
{
    public static string CleanText(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        string decoded = WebUtility.HtmlDecode(input);
        var cleaned = Regex.Replace(decoded,@"\s+", " ").Trim();
        return cleaned;
    }
}