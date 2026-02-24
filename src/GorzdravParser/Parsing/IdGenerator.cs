using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace GorzdravParser.Parsing;

public class IdGenerator
{
    public string Generate(IWebElement card)
    {
        try
        {
            var link = card.FindElement(By.CssSelector(".product-card-body__title"));
            var href = link.GetAttribute("href");

            if (href.Contains("/bundle_"))
            {
                var bundleMatch = Regex.Match(href, @"-(\d+-\d+)/?$");
                if (bundleMatch.Success)
                {
                    return bundleMatch.Groups[1].Value;
                }
            }
            
            var regularMatch = Regex.Match(href, @"-(\d+)/?$");
            if (regularMatch.Success)
            {
                return regularMatch.Groups[1].Value;
            }
            
            var fallbackMatch = Regex.Match(href, @"/(\d+)/?$");
            if (fallbackMatch.Success)
            {
                return fallbackMatch.Groups[1].Value;
            }
            
            return "";
        }
        catch
        {
            return "";
        }
    }
}
