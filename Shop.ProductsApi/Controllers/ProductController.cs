using Microsoft.AspNetCore.Mvc;
using Shop.ProductsApi.Interfaces;
using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        
        [HttpPost("createProduct")]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (_categoryService.CategoryExists(product.CategoryName))
            {
                _productService.CreateProduct(product);
                return Created($"api/products/{product.Id}", product);
            }
            return BadRequest("CategoryName property alludes to non-existening category.");
            
        }

        [HttpGet("{getProductById}")]
        public ActionResult<Product> GetProductById(uint id)
        {
            if (!_productService.ProductIdExists(id))
            {
                return NotFound($"Product of given ID = {id} does not exist.");
            }
            var product = _productService.GetProduct(id);
            return Ok(product);
        }
        
        [HttpGet("getAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAllProducts() => Ok(_productService.GetAllProducts());

        [HttpPut("updateProduct")]
        public ActionResult<Product> UpdateProduct(uint id, Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return BadRequest($"id parameter does not match the product id\n{id} != {updatedProduct.Id}");
            }

            if (!_productService.ProductIdExists(id))
            {
                return NotFound($"Product of given ID = {id} does not exist.");
            }

            var product = _productService.UpdateProduct(updatedProduct);

            return Accepted($"api/products/{product.Id}", product);
        }

        [HttpDelete("deleteProduct")]
        public IActionResult DeleteProduct(uint id)
        {
            if (!_productService.ProductIdExists(id))
            {
                return NotFound($"Product of given ID = {id} does not exist.");
            }

            _productService.DeleteProduct(id);

            return Ok($"api/products/{id}");
        }
    }
}
