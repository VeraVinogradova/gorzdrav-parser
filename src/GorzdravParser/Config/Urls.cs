namespace GorzdravParser.Config;

public class Urls
{
    private readonly string _base = "https://gorzdrav.org";
    
    public string Catalog => $"{_base}/category/sredstva-ot-diabeta/";
    public string ForRegion(string region) => region == "Москва" 
        ? Catalog 
        : $"{_base}/kaliningrad/category/sredstva-ot-diabeta/";
}