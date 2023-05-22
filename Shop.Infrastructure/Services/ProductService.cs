using Shop.Application.Core.Services;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Data;
using Shop.ProductsApi.Data;

namespace Shop.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _productServiceBaseUrl;
        private readonly AppDbContext _appDbContext;
        
        public ProductService(AppDbContext appDbContext)
        {
            _httpClient= new HttpClient();
            _productServiceBaseUrl = "";
            _appDbContext = appDbContext;
        }

        public Product CreateProduct(Product product)
        {
            return new Product()
            {
                Category = product.Category,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ID = product.ID
            };
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
