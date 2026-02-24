using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using GorzdravParser.Config;

namespace GorzdravParser.Browser;

public class DriverFactory
{
    private readonly Settings _settings;
    
    public DriverFactory(Settings settings) => _settings = settings;
    
    public IWebDriver Create()
    {
        var options = new ChromeOptions();
        
        if (_settings.Headless)
            options.AddArgument("--headless=new");
        
        options.AddExcludedArgument("enable-automation");
        options.AddAdditionalOption("useAutomationExtension", false);
        options.AddArgument("--disable-blink-features=AutomationControlled");
        
        return new ChromeDriver(options);
    }
}