using Newtonsoft.Json;
using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _productServiceBaseUrl = "http://localhost:5170/api/products";
        }

        public Product CreateProduct(Product product)
        {
            var url = $"{_productServiceBaseUrl}/";

            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var url = $"{_productServiceBaseUrl}/getAllProducts";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                var responseContext = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(responseContext); //new List<Product>();

                return products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        //public Product GetProduct(int productId)
        //{
        //    var url = $"{_productServiceBaseUrl}/";
        //    throw new NotImplementedException();
        //}

        public Product GetProductById(int id)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }

        //public bool ProductExists(int productId)
        //{
        //    var url = $"{_productServiceBaseUrl}/";
        //    throw new NotImplementedException();
        //}

        //public void SaveProduct(Product product)
        //{
        //    var url = $"{_productServiceBaseUrl}/";
        //    throw new NotImplementedException();
        //}

        public Product UpdateProduct(Product product)
        {
            var url = $"{_productServiceBaseUrl}/";
            throw new NotImplementedException();
        }
    }
}
