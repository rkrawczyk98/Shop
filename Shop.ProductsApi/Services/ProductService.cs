using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shop.ProductsApi.Data;
using Shop.ProductsApi.Interfaces;
using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        
        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Product CreateProduct(Product product)
        {
            if (ProductIdExists(product.Id))
            {
                throw new Exception("Product of that ID already exists.");
            }
            _appDbContext.Add(product);
            _appDbContext.SaveChanges();
            return product;
        }

        public void DeleteProduct(uint id)
        {
            var existingProduct = GetProduct(id);
            if (existingProduct == null)
            {
                throw new Exception("Product does not exist.");
            }

            _appDbContext.Products.Remove(existingProduct);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _appDbContext.Products;
        }

        public Product GetProduct(uint productId)
        {
            if (!ProductIdExists(productId))
            {
                throw new Exception("Product does not exist.");
            }
            return _appDbContext.Products.Find(productId);
        }
        
        public bool ProductIdExists(uint productId)
        {
            return _appDbContext.Products.Any(p => p.Id == productId);
        }

        public void SaveProduct(Product product)
        {
            if (ProductIdExists(product.Id))
            {
                UpdateProduct(product);
            }
            else
            {
                CreateProduct(product);
            }
        }

        public Product UpdateProduct(Product product)
        {
            var existingProduct = GetProduct(product.Id);
            if (existingProduct == null)
            {
                throw new Exception("Product does not exist.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            existingProduct.CategoryName = product.CategoryName;
            existingProduct.Stock = product.Stock;
            
            _appDbContext.SaveChanges();
            return existingProduct;
        }
    }
}
