using Microsoft.EntityFrameworkCore;
using Shop.ProductsApi.Data;
using Shop.ProductsApi.Interfaces;
using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Services;

public class CategoryService : ICategoryService
{
    private readonly string _categoryServiceBaseUrl;
    private readonly AppDbContext _appDbContext;
        
    public CategoryService(AppDbContext appDbContext)
    {
        _categoryServiceBaseUrl = "";
        _appDbContext = appDbContext;
    }
    public Category CreateCategory(Category category)
    {
        var url = $"{_categoryServiceBaseUrl}/";
        if (CategoryExists(category.Name))
        {
            throw new Exception("Category with that name already exists.");
        }

        _appDbContext.Add(category);
        _appDbContext.SaveChanges();
        return category;
    }

    public Category GetCategory(string categoryName)
    {
        var url = $"{_categoryServiceBaseUrl}/";
        if (!CategoryExists(categoryName))
        {
            throw new Exception("Category does not exist.");
        }

        var result = _appDbContext.Categories
            .Include(c=>c.Products)
            .FirstOrDefault(c => c.Name == categoryName);
        if (result == null)
            throw new Exception("Category is null");

        return result;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        var url = $"{_categoryServiceBaseUrl}/";
        return _appDbContext.Categories;
    }

    public void SaveCategory(Category category)
    {
        var url = $"{_categoryServiceBaseUrl}/";
        if (CategoryExists(category.Name))
        {
            UpdateCategory(category);
        }
        else
        {
            CreateCategory(category);
        }
    }


    public Category UpdateCategory(Category category)
    {
        var url = $"{_categoryServiceBaseUrl}/";

        if (!CategoryExists(category.Name))
        {
            throw new Exception("Category of that Name does not exist");
        }
        var existingCategory = GetCategory(category.Name);
        if (existingCategory == null)
        {
            throw new Exception("Category of that Name is null.");
        }
        
        existingCategory.Description = category.Description;
        existingCategory.Products = category.Products;

        _appDbContext.Update(existingCategory);
        _appDbContext.SaveChanges();
        return existingCategory;
    }


    public void DeleteCategory(string categoryName)
    {
        var url = $"{_categoryServiceBaseUrl}/";
        if (!CategoryExists(categoryName))
        {
            throw new Exception("Category does not exist.");
        }
        _appDbContext.Categories.Remove(GetCategory(categoryName));
        _appDbContext.SaveChanges();
    }
    public bool CategoryExists(string categoryName)
    {
        var url = $"{_categoryServiceBaseUrl}/";
        return _appDbContext.Categories.Any(c => c.Name == categoryName);
    }
}