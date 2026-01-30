namespace WebScraperApp;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    
    public Product() { }
    
    public Product(string name, double price)
    {
        Name = name;
        Price = price;
    }
    public override string ToString()
    {
        return $"{Name} - {Price} EUR\n";
    }
}