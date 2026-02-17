using OpenQA.Selenium;
using GorzdravParser.Config;

namespace GorzdravParser.Browser;

public class PageLoader
{
    private readonly IWebDriver _driver;
    private readonly HumanSimulator _human;
    private readonly Settings _settings;
    
    public PageLoader(DriverFactory factory, HumanSimulator human, Settings settings)
    {
        _driver = factory.Create();
        _human = human;
        _settings = settings;
    }
    
    public async Task LoadAsync(string url)
    {
        _driver.Navigate().GoToUrl(url);
        await _human.WaitAsync(_settings.MinDelayMs, _settings.MaxDelayMs);
    }
    
    public IWebDriver Driver => _driver;
}