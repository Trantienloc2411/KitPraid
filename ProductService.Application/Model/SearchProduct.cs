namespace ProductService.Application.Model;

public class SearchProduct
{
    public string Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string Sku { get; set; }
    public string CategoryId { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    
}