using Shop.Application.Services;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _productServiceBaseUrl;

        public ProductRepository()
        {
            _httpClient= new HttpClient();
            _productServiceBaseUrl = "";
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var url = $"{_productServiceBaseUrl}/"; // dodać reszte adresu do metody w mikroserwisie 
            throw new NotImplementedException();
        }

        public Product GetProduct(int productId)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }

        public void SaveProduct(Product product)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }
    }
}
