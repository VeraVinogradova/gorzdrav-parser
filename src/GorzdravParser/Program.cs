using GorzdravParser.Browser;
using GorzdravParser.Config;
using GorzdravParser.Parsing;
using GorzdravParser.Storage;
using GorzdravParser.Workflow;

Console.WriteLine("1 | Москва");
Console.WriteLine("2 | Калининград");
var key = Console.ReadKey();
Console.WriteLine();

string region = key.KeyChar == '1' ? "Москва" : "Калининград";

var settings = Settings.Load("appsettings.json");
var driverFactory = new DriverFactory(settings);
var pageLoader = new PageLoader(driverFactory, new HumanSimulator(), settings);

try
{
    var productSet = new ProductSet();

    var parser = new Parser(
        new SiteNavigator(pageLoader, new Urls()),
        new PageCrawler(pageLoader, 
            new ProductParser(new PriceCleaner(), new IdGenerator()),
            productSet),
        productSet,
        new CsvWriter()
    );

    await parser.RunAsync(region);
}
finally
{
    pageLoader.Driver.Quit();
    Console.WriteLine("Готово");
}