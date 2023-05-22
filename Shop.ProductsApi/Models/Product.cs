namespace Shop.ProductsApi.Models
{
    public class Product
    {
        public uint ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public string Price { get; set; }
        public int Stock { get; set; }
    }
}
