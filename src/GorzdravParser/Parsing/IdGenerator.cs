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

            var match = Regex.Match(href, @"-(\d+)/?$");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            match = Regex.Match(href, @"/(\d+)/?$");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            
            return "";
        }
        catch
        {
            return "";
        }
    }
}