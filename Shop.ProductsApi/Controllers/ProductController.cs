using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.ProductsApi.Data;
using Shop.ProductsApi.Interfaces;
using Shop.ProductsApi.Models;
using Shop.ProductsApi.Services;

namespace Shop.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("GetProductById")]
        public ActionResult<Product> GetProductById(uint id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public ActionResult<Product> CreateProduct(string name, string description, decimal price, int stock, string categoryName)
        {
            
            var newProduct = _productService.CreateProduct(new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                Stock = stock,
                CategoryName = categoryName,
            });
            return Created($"api/products/{newProduct.Id}", newProduct);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(uint id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (!_productService.ProductIdExists(id))
            {
                return NotFound();
            }

            _productService.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(uint id)
        {
            if (!_productService.ProductIdExists(id))
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);

            return NoContent();
        }
        
    }
}
