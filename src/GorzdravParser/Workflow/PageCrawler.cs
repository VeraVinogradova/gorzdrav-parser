using OpenQA.Selenium;
using GorzdravParser.Browser;
using GorzdravParser.Parsing;
using GorzdravParser.Storage;

namespace GorzdravParser.Workflow;

public class PageCrawler
{
    private readonly PageLoader _loader;
    private readonly ProductParser _parser;
    private readonly ProductSet _storage;
    private string _lastUrl = "";
    
    public PageCrawler(PageLoader l, ProductParser p, ProductSet s) 
        => (_loader, _parser, _storage) = (l, p, s);
    
    public async Task<int> ParseCurrentPageAsync(string region)
    {
        var currentUrl = _loader.Driver.Url;
        if (currentUrl == _lastUrl) return -1;
        _lastUrl = currentUrl;
        
        await Task.Delay(1000);
        
        var cards = _loader.Driver.FindElements(By.CssSelector(".product-card"));
        var productList = new List<GorzdravParser.Models.Product>();
        
        foreach (var card in cards)
        {
            var product = _parser.Parse(card, region);
            productList.Add(product);
        }
        
        _storage.Add(productList);
        return productList.Count;
    }
    
    public bool HasNextPage()
    {
        try
        {
            return _loader.Driver.FindElements(By.CssSelector("li.ui-table-pagination__arrow-container:last-child")).Any();
        }
        catch { return false; }
    }
    
    public async Task GoToNextPageAsync()
    {
        try
        {
            var arrow = _loader.Driver.FindElement(By.CssSelector("li.ui-table-pagination__arrow-container:last-child"));
            ((IJavaScriptExecutor)_loader.Driver).ExecuteScript("arguments[0].click();", arrow);
            await Task.Delay(2000);
        }
        catch { }
    }
    
    public void Reset() => _lastUrl = "";
}