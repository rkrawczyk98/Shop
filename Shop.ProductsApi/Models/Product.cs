namespace Shop.ProductsApi.Models
{
    public class Product
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public string CategoryName { get; set; }
        public Category? Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
