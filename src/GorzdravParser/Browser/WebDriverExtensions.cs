using OpenQA.Selenium;

namespace GorzdravParser.Browser;

public static class WebDriverExtensions
{
    public static void ClickViaJs(this IWebDriver driver, IWebElement element) =>
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
}