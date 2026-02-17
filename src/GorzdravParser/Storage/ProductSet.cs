using System.Collections.Concurrent;
using GorzdravParser.Models;

namespace GorzdravParser.Storage;

public class ProductSet
{
    private readonly ConcurrentDictionary<string, Product> _products = new();
    
    public void Add(IEnumerable<Product> products)
    {
        foreach (var p in products)
        {
            if (p != null && !string.IsNullOrEmpty(p.Id))
            {
                if (_products.TryGetValue(p.Id, out var existing))
                {
                    if (string.IsNullOrEmpty(existing.Name) && !string.IsNullOrEmpty(p.Name))
                        existing.Name = p.Name;
                    if (string.IsNullOrEmpty(existing.Company) && !string.IsNullOrEmpty(p.Company))
                        existing.Company = p.Company;
                    if (string.IsNullOrEmpty(existing.Substance) && !string.IsNullOrEmpty(p.Substance))
                        existing.Substance = p.Substance;
                    if (string.IsNullOrEmpty(existing.CurrentPrice) && !string.IsNullOrEmpty(p.CurrentPrice))
                        existing.CurrentPrice = p.CurrentPrice;
                }
                else
                {
                    _products[p.Id] = p;
                }
            }
        }
    }
    
    public List<Product> GetAll() => _products.Values.ToList();
    
    public void Clear() => _products.Clear();
    
    public int Count => _products.Count;
}