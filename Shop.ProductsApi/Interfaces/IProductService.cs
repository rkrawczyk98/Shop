using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Interfaces
{
        public interface IProductService
        {
            Product GetProduct(int productId);
            IEnumerable<Product> GetAllProducts();
            void SaveProduct(Product product);
            Product GetProductById(int id);
            Product CreateProduct(Product product);
            Product UpdateProduct(Product product);
            bool ProductExists(int productId);
            void DeleteProduct(int id);
        }
}
