using GorzdravParser.Browser;
using GorzdravParser.Config;
using GorzdravParser.Parsing;
using GorzdravParser.Storage;
using GorzdravParser.Workflow;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var urls = new Urls(config);
var regions = urls.GetAllRegions();

Console.WriteLine("Выберите регион:");
for (int i = 0; i < regions.Count; i++)
{
    Console.WriteLine($"{i + 1} - {regions[i]}");
}

var key = Console.ReadKey();
Console.WriteLine();

var selectedRegion = regions[int.Parse(key.KeyChar.ToString()) - 1];
Console.WriteLine($"Выбран: {selectedRegion}");

var settings = Settings.Load("appsettings.json");
var driverFactory = new DriverFactory(settings);
var pageLoader = new PageLoader(driverFactory, new HumanSimulator(), settings);

try
{
    var productSet = new ProductSet();

    var parser = new Parser(
        new SiteNavigator(pageLoader, urls),
        new PageCrawler(pageLoader, 
            new ProductParser(new PriceCleaner(), new IdGenerator()),
            productSet),
        productSet,
        new CsvWriter(),
        urls
    );

    await parser.RunAsync(selectedRegion);
}
finally
{
    pageLoader.Driver.Quit();
    Console.WriteLine("Браузер закрыт");
}