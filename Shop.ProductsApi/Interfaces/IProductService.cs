using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Interfaces;

public interface IProductService
{
    Product GetProduct(uint productId);
    IEnumerable<Product> GetAllProducts();
    void SaveProduct(Product product);
    Product CreateProduct(Product product);
    Product UpdateProduct(Product product);
    bool ProductIdExists(uint productId);
    void DeleteProduct(uint productId);
}