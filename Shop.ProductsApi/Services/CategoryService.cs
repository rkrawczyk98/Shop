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

    public Category GetCategory(string categoryName)
    {
        return _appDbContext.Categories.FirstOrDefault(c => c.Name == categoryName);
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _appDbContext.Categories;
    }

    public void SaveCategory(Category category)
    {
        if (CategoryExists(category.Name))
        {
            UpdateCategory(category.Name, category.Description);
        }
        else
        {
            CreateCategory(category);
        }
    }

    public Category CreateCategory(Category category)
    {
        if (CategoryExists(category.Name))
        {
            throw new Exception("Category already exists.");
        }

        _appDbContext.Add(category);
        _appDbContext.SaveChanges();
        return category;
    }

    public Category UpdateCategory(string categoryName, string categoryDescription)
    {
        var existingCategory = GetCategory(categoryName);
        if (existingCategory == null)
        {
            throw new Exception("Category does not exist.");
        }

        existingCategory.Description = categoryDescription;
        _appDbContext.SaveChanges();
        return existingCategory;
    }

    public bool CategoryExists(string categoryName)
    {
        return _appDbContext.Categories.Any(c => c.Name == categoryName);
    }

    public void DeleteCategory(string categoryName)
    {
        var existingCategory = GetCategory(categoryName);
        if (existingCategory == null)
        {
            throw new Exception("Category does not exist.");
        }

        _appDbContext.Categories.Remove(existingCategory);
        _appDbContext.SaveChanges();
    }
}