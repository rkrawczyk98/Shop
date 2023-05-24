using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Shop.ProductsApi.Interfaces;
using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetCategory")]
        public ActionResult<Category> GetCategory(string name)
        {
            var category = _categoryService.GetCategory(name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpGet("GetAllCategories")]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost("CreateCategory")]
        public ActionResult<Category> CreateCategory(string name, string description)
        {
            if (name == null || description == null)
                return BadRequest();
            var createdCategory = _categoryService.CreateCategory( new Category(){Name = name, Description = description});
            return Created($"api/categories/{createdCategory.Name}", createdCategory);
        }

        [HttpPut("UpdateCategory")]
        public ActionResult<Category> UpdateCategory(string categoryName, string newDescription)
        {
            var updatedCategory = _categoryService.UpdateCategory(categoryName, newDescription);
            return Ok(updatedCategory);
        }

        [HttpDelete("DeleteCategory")]
        public ActionResult DeleteCategory(string categoryName)
        {
            _categoryService.DeleteCategory(categoryName);
            return Ok();
        }
    }
}