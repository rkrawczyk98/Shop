namespace Shop.ProductsApi.Models;

public class Category
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; }
}