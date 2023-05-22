using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Services;
using Shop.Domain.Entities;
using Shop.ProductsApi.Data;

namespace Shop.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productRepository;

        public ProductController(IProductService productRepository, AppDbContext context)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("GetProductById")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public ActionResult<Product> CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ID }, product);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }

            _productRepository.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(id);

            return NoContent();
        }
    }
}
