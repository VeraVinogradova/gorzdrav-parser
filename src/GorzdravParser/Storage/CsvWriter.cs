using System.Globalization;
using CsvHelper;
using GorzdravParser.Models;

namespace GorzdravParser.Storage;

public class CsvWriter
{
    public async Task WriteAsync(string path, List<Product> products)
    {
        var resultsDir = Path.Combine(Directory.GetCurrentDirectory(), "results");
        if (!Directory.Exists(resultsDir))
        {
            Directory.CreateDirectory(resultsDir);
        }
        
        var fullPath = Path.Combine(resultsDir, path);
        
        using var writer = new StreamWriter(fullPath);
        using var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
        
        csv.WriteField("ID товара");
        csv.WriteField("Название препарата");
        csv.WriteField("Рецептурность препарата");
        csv.WriteField("Производитель");
        csv.WriteField("Активное вещество");
        csv.WriteField("Текущая цена");
        csv.WriteField("Старая цена");
        csv.WriteField("Ссылка на картинку");
        csv.WriteField("Ссылка на товар");
        csv.WriteField("Название региона");
        await csv.NextRecordAsync();
        
        foreach (var p in products)
        {
            csv.WriteField(p.Id);
            csv.WriteField(p.Name);
            csv.WriteField(p.Recipe ?? "");
            csv.WriteField(p.Company ?? "");
            csv.WriteField(p.Substance ?? "");
            csv.WriteField(p.CurrentPrice ?? "");
            csv.WriteField(p.OldPrice ?? "");
            csv.WriteField(p.ImageUrl ?? "");
            csv.WriteField(p.ProductUrl ?? "");
            csv.WriteField(p.Region ?? "");
            await csv.NextRecordAsync();
        }
    }
}