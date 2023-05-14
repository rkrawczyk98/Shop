using Microsoft.AspNetCore.Mvc;
using Shop.Application.Services;
using Shop.Domain.Entities;

namespace Shop.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            _productRepository.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ID }, product);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
