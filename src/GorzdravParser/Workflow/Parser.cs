using GorzdravParser.Storage;

namespace GorzdravParser.Workflow;

public class Parser
{
    private readonly SiteNavigator _navigator;
    private readonly PageCrawler _crawler;
    private readonly ProductSet _storage;
    private readonly CsvWriter _csv;
    
    public Parser(SiteNavigator nav, PageCrawler crawl, ProductSet store, CsvWriter csv) 
        => (_navigator, _crawler, _storage, _csv) = (nav, crawl, store, csv);
    
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
        
        var fileName = region == "Москва" ? "result.csv" : $"result_{region}.csv";
        var products = _storage.GetAll();
        await _csv.WriteAsync(fileName, products);
        Console.WriteLine($"Всего товаров: {products.Count}\n");
        _storage.Clear();
    }
}