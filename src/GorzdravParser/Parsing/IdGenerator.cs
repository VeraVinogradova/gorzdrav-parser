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

<<<<<<< HEAD
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
=======
            var match = Regex.Match(href, @"-(\d+)/?$");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            match = Regex.Match(href, @"/(\d+)/?$");
            if (match.Success)
            {
                return match.Groups[1].Value;
>>>>>>> 3bd07aef3ebf8bf6557b9672b5c0b049103a716e
            }
            
            return "";
        }
        catch
        {
            return "";
        }
    }
}