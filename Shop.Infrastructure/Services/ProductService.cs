using Newtonsoft.Json.Linq;
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
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService(HttpClient client) 
        {
            _client = client;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            try
            {
                var data = new JObject { ["product"] = product };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync($"http://localhost:5170/api/products/createProduct/{product}");
                var responseContent = await response.Content.ReadAsStringAsync();
                var responeProduct = JsonConvert.DeserializeObject<Product>(responseContent);

                return responeProduct;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var data = new JObject { ["product"] = product };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/products/createProduct", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responeProduct = JsonConvert.DeserializeObject<Product>(responseContent);

                return responeProduct;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var data = new JObject { ["product"] = product };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/products/createProduct", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responeProduct = JsonConvert.DeserializeObject<Product>(responseContent);

                return responeProduct;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        //public Product GetProduct(int productId)
        //{
        //    throw new NotImplementedException();
        //}

        public Product GetProductById(int id)
        {
            try
            {
                var data = new JObject { ["product"] = product };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/products/createProduct", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responeProduct = JsonConvert.DeserializeObject<Product>(responseContent);

                return responeProduct;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        //public bool ProductExists(int productId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SaveProduct(Product product)
        //{
        //    throw new NotImplementedException();
        //}

        public Product UpdateProduct(Product product)
        {
            try
            {
                var data = new JObject { ["product"] = product };
                var contentdata = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("http://localhost:5170/api/products/createProduct", contentdata);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responeProduct = JsonConvert.DeserializeObject<Product>(responseContent);

                return responeProduct;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
