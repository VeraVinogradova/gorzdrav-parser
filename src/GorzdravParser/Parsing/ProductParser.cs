using OpenQA.Selenium;
using GorzdravParser.Models;
using System.Text.RegularExpressions;

namespace GorzdravParser.Parsing;

public class ProductParser
{
    private readonly PriceCleaner _priceCleaner;
    private readonly IdGenerator _idGen;
    
    public ProductParser(PriceCleaner p, IdGenerator id) 
    {
        _priceCleaner = p;
        _idGen = id;
    }
    
    public Product Parse(IWebElement card, string region)
    {
        var product = new Product { Region = region };
        var js = (IJavaScriptExecutor)((IWrapsDriver)card).WrappedDriver;

        try
        {
            var linkElement = card.FindElement(By.CssSelector(".product-card-body__title"));
            var href = linkElement.GetAttribute("href");
            product.ProductUrl = href.StartsWith("/") ? "https://gorzdrav.org" + href : href;
            
            var idMatch = Regex.Match(product.ProductUrl, @"-(\d+)/?$");
            product.Id = idMatch.Success ? idMatch.Groups[1].Value : "";
            
            product.Name = (string)js.ExecuteScript("return arguments[0].textContent;", linkElement);
            
            try
            {
                var prescription = js.ExecuteScript(@"
                    var chips = arguments[0].querySelectorAll('.custom-chip__text');
                    for(var i=0; i<chips.length; i++) {
                        if(chips[i].textContent.includes('По рецепту')) return 'По рецепту';
                    }
                    return '';
                ", card)?.ToString() ?? "";
                product.Prescription = prescription;
            }
            catch { }
            
            try
            {
                product.Manufacturer = js.ExecuteScript(@"
                    var items = arguments[0].querySelectorAll('.product-card__item');
                    for(var i=0; i<items.length; i++) {
                        if(items[i].textContent.includes('Производитель')) {
                            var link = items[i].querySelector('a');
                            return link ? link.textContent.trim() : '';
                        }
                    }
                    return '';
                ", card)?.ToString() ?? "";
            }
            catch { }
            
            try
            {
                product.Substance = js.ExecuteScript(@"
                    var items = arguments[0].querySelectorAll('.product-card__item');
                    for(var i=0; i<items.length; i++) {
                        if(items[i].textContent.includes('Действующее вещество')) {
                            var link = items[i].querySelector('a');
                            return link ? link.textContent.trim() : '';
                        }
                    }
                    return '';
                ", card)?.ToString() ?? "";
            }
            catch { }
            
            try
            {
                var priceText = js.ExecuteScript(@"
                    var priceEl = arguments[0].querySelector('.ui-price__price');
                    return priceEl ? priceEl.textContent : '';
                ", card)?.ToString() ?? "";
                
                if (!string.IsNullOrWhiteSpace(priceText))
                {
                    product.Price = _priceCleaner.Clean(priceText);
                }
                
                var oldPriceText = js.ExecuteScript(@"
                    var oldPriceEl = arguments[0].querySelector('.ui-price__discount-value');
                    return oldPriceEl ? oldPriceEl.textContent : '';
                ", card)?.ToString() ?? "";
                
                if (!string.IsNullOrWhiteSpace(oldPriceText))
                {
                    product.OldPrice = _priceCleaner.Clean(oldPriceText);
                }
            }
            catch { }
            
            try
            {
                var src = js.ExecuteScript(@"
                    var img = arguments[0].querySelector('.product-card-image__img');
                    return img ? img.getAttribute('src') : '';
                ", card)?.ToString() ?? "";
                product.ImageUrl = src.StartsWith("/") ? "https://gorzdrav.org" + src : src;
            }
            catch { }
        }
        catch
        {
        }

        return product;
    }
}