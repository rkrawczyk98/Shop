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

            product.Category = _appDbContext.Categories.FirstOrDefault(c => c.Name == product.CategoryName);
                
            _appDbContext.Add(product);
            _appDbContext.SaveChanges();
            return product;
        }

        public Product GetProduct(uint productId)
        {

            if (!ProductIdExists(productId))
           //var existingProduct = GetProduct(id);
           // if (existingProduct == null)
            {
                throw new Exception("Product does not exist.");
            }
            
            var result = _appDbContext.Products
                .Include(p=>p.Category)
                .FirstOrDefault(p=>p.Id == productId);
            
            if (result == null)
                throw new Exception("Product is null");
            
            return result;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _appDbContext.Products;
        }


//        public Product GetProduct(uint productId)
//        {
//            if (!ProductIdExists(productId))
//            {
//                throw new Exception("Product does not exist.");
//            }
//            return _appDbContext.Products.Find(productId);
//        }
//        
//        public bool ProductIdExists(uint productId)
//        {
//            return _appDbContext.Products.Any(p => p.Id == productId);
//        }
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

            if (!ProductIdExists(product.Id))
            {
                throw new Exception("Product of that ID does not exist.");
            }
            var existingProduct = GetProduct(product.Id);
            if (existingProduct == null)
            {
                throw new Exception("Product of that ID is null.");

            //var existingProduct = GetProduct(product.Id);
            //if (existingProduct == null)
            //{
                //throw new Exception("Product does not exist.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.CategoryName = product.CategoryName;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            _appDbContext.Update(existingProduct);
            _appDbContext.SaveChanges();
            return existingProduct;
        }
        public void DeleteProduct(uint id)
        {
            if (!ProductIdExists(id))
            {
                throw new Exception("Product does not exist.");
            }
            
            _appDbContext.Products.Remove(GetProduct(id));
            _appDbContext.SaveChanges();
        }
        public bool ProductIdExists(uint productId)
        {
            return _appDbContext.Products.Any(p => p.Id == productId);
        }
    }
}
