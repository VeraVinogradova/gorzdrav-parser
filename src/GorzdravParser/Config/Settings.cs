using Microsoft.Extensions.Configuration;

namespace GorzdravParser.Config;

public class Settings
{
    public string BaseUrl { get; private set; }
    public int MinDelayMs { get; private set; }
    public int MaxDelayMs { get; private set; }
    public bool Headless { get; private set; }

    public static Settings Load(string path)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile(path)
            .Build();
        
        return new Settings
        {
            BaseUrl = config["Site:BaseUrl"],
            MinDelayMs = int.Parse(config["Human:MinDelayMs"]),
            MaxDelayMs = int.Parse(config["Human:MaxDelayMs"]),
            Headless = bool.Parse(config["Browser:Headless"])
        };
    }
}