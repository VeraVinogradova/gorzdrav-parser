using OpenQA.Selenium;
using GorzdravParser.Browser;
using GorzdravParser.Config;

namespace GorzdravParser.Workflow;

public class SiteNavigator
{
    private readonly PageLoader _loader;
    private readonly Urls _urls;
    
    public SiteNavigator(PageLoader loader, Urls urls) 
        => (_loader, _urls) = (loader, urls);
    
    public async Task GoToRegionAsync(string region)
    {
        var url = _urls.ForRegion(region);
        await _loader.LoadAsync(url);
    }
    
    public IWebDriver Driver => _loader.Driver;
}