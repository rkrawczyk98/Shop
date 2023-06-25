using Microsoft.AspNetCore.Mvc;
using Shop.ProductsApi.Interfaces;
using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpPost("CreateCategory")]
        public ActionResult<Category> CreateCategory(Category category)
        {
            if (_categoryService.CategoryExists(category.Name))
            {
                return BadRequest("Category with that name already exists.");
            }
            var createdCategory = _categoryService.CreateCategory(category);
            return Created($"api/categories/{createdCategory.Name}", createdCategory);
        }

        [HttpGet("{categoryName}")]
        public ActionResult<Category> GetCategory(string categoryName)
        {
            if (!_categoryService.CategoryExists(categoryName))
            {
                return NotFound("Category with that name does not exist.");
            }
            var category = _categoryService.GetCategory(categoryName);
            return Ok(category);
        }
        
        [HttpGet("GetAllCategories")]
        public ActionResult<IEnumerable<Category>> GetAllCategories() => Ok(_categoryService.GetAllCategories());

        [HttpPut("Update/{categoryName}")]
        public ActionResult<Category> UpdateCategory(string categoryName, Category category)
        {
            if (categoryName != category.Name)
            {
                return BadRequest($"categoryName parameter does not match the category\n{categoryName} != {category.Name}");
            }

            if (!_categoryService.CategoryExists(categoryName))
            {
                return NotFound($"Category of given Name = {categoryName} does not exist.");
            }
            
            var updatedCategory = _categoryService.UpdateCategory(category);
            return Accepted($"api/categories/{updatedCategory.Name}", updatedCategory);
        }

        [HttpDelete("Delete/{categoryName}")]
        public ActionResult DeleteCategory(string categoryName)
        {
            if (!_categoryService.CategoryExists(categoryName))
            {
                return NotFound($"Category of given Name = {categoryName} does not exist.");
            }
            
            _categoryService.DeleteCategory(categoryName);
            
            return Ok($"api/categories/{categoryName}");
        }
    }
}