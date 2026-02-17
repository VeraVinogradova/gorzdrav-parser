using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GorzdravParser.Browser;

public class HumanSimulator
{
    private readonly Random _random = new();
    private readonly Actions _actions;
    
    public HumanSimulator(IWebDriver driver = null)
    {
        if (driver != null)
            _actions = new Actions(driver);
    }
    
    public async Task WaitAsync(int minMs, int maxMs)
    {
        var delay = _random.Next(minMs, maxMs);
        await Task.Delay(delay);
    }
    
    public void MoveAndClick(IWebElement element)
    {
        if (_actions == null) return;
        
        _actions.MoveToElement(element)
                .Pause(TimeSpan.FromMilliseconds(_random.Next(200, 500)))
                .Click()
                .Perform();
    }
}