﻿using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public interface IProductRepository
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
