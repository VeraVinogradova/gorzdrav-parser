using System.Globalization;
using CsvHelper;
using GorzdravParser.Models;

namespace GorzdravParser.Storage;

public class CsvWriter
{
    public async Task WriteAsync(string path, List<Product> products)
    {
        using var writer = new StreamWriter(path);
        using var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
        
        await csv.WriteRecordsAsync(products);
    }
}