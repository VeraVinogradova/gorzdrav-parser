using GorzdravParser.Storage;
using GorzdravParser.Config;

namespace GorzdravParser.Workflow;

public class Parser
{
    private readonly SiteNavigator _navigator;
    private readonly PageCrawler _crawler;
    private readonly ProductSet _storage;
    private readonly CsvWriter _csv;
    private readonly Urls _urls;
    
    public Parser(SiteNavigator nav, PageCrawler crawl, ProductSet store, CsvWriter csv, Urls urls) 
        => (_navigator, _crawler, _storage, _csv, _urls) = (nav, crawl, store, csv, urls);
    
    public async Task RunAsync(string region)
    {
        Console.WriteLine($"\nПарсинг: {region}");
        await _navigator.GoToRegionAsync(region);
        _crawler.Reset();
        
        while (true)
        {
            var count = await _crawler.ParseCurrentPageAsync(region);
            if (count == -1) break;
            Console.WriteLine($"{count} товаров");
            
            if (!_crawler.HasNextPage()) break;
            await _crawler.GoToNextPageAsync();
        }
        
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        var regionFile = _urls.GetFileName(region);
        var fileName = $"result_{regionFile}_{timestamp}.csv";
            
        var products = _storage.GetAll();
        await _csv.WriteAsync(fileName, products);
        Console.WriteLine($"Всего товаров: {products.Count}\n");
        _storage.Clear();
    }
}