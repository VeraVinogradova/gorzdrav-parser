namespace GorzdravParser.Models;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Prescription { get; set; }
    public string Manufacturer { get; set; }
    public string Substance { get; set; }
    public string Price { get; set; }
    public string OldPrice { get; set; }
    public string ImageUrl { get; set; }
    public string ProductUrl { get; set; }
    public string Region { get; set; }
}