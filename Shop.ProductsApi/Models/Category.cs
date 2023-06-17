namespace Shop.ProductsApi.Models;

#pragma warning disable CS8618
public class Category
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Product>? Products { get; set; }
}
#pragma warning restore CS8618