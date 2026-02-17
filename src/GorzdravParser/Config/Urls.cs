using Microsoft.Extensions.Configuration;

namespace GorzdravParser.Config;

public class Urls
{
    private readonly IConfiguration _config;
    private readonly string _baseUrl;
    private readonly string _catalogPath;
    
    public Urls(IConfiguration config)
    {
        _config = config;
        _baseUrl = config["Site:BaseUrl"];
        _catalogPath = config["Site:CatalogPath"];
    }
    
    public string ForRegion(string region)
    {
        var urlPrefix = _config[$"Regions:{region}:UrlPrefix"];
        
        if (string.IsNullOrEmpty(urlPrefix))
            return _baseUrl + _catalogPath;
        
        return $"{_baseUrl}/{urlPrefix}{_catalogPath}";
    }
    
    public string GetFileName(string region)
    {
        return _config[$"Regions:{region}:FileName"] ?? region.ToLower();
    }
    
    public List<string> GetAllRegions()
    {
        return _config.GetSection("Regions").GetChildren().Select(x => x.Key).ToList();
    }
}