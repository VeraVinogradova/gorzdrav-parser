using System.Text.RegularExpressions;

namespace GorzdravParser.Parsing;

public class PriceCleaner
{
    public string Clean(string priceText)
    {
        if (string.IsNullOrWhiteSpace(priceText)) return "";
        
        var withoutSpaces = Regex.Replace(priceText, @"\s+", "");
        
        var match = Regex.Match(withoutSpaces, @"[\d.]+");
        if (!match.Success) return "";
        
        var cleaned = match.Value;

        if (cleaned.Contains('.'))
        {
            cleaned = cleaned.Split('.')[0];
        }
        
        return cleaned;
    }
}