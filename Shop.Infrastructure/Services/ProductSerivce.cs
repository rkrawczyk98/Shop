using Shop.Application.Core.Services;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class ProductSerivce : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _productServiceBaseUrl;

        public ProductSerivce()
        {
            _httpClient= new HttpClient();
            _productServiceBaseUrl = "";
        }

        public Product CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
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

        public Product GetProductById(int id)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }

        public bool ProductExists(int productId)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }

        public void SaveProduct(Product product)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product product)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }
    }
}
