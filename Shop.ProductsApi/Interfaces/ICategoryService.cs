﻿using Shop.ProductsApi.Models;

namespace Shop.ProductsApi.Interfaces;

public interface ICategoryService
{
    Category GetCategory(string categoryName);
    IEnumerable<Category> GetAllCategories();
    void SaveCategory(Category category);
    Category CreateCategory(Category category);
    Category UpdateCategory(string categoryName, string categoryDescription);
    bool CategoryExists(string categoryName);
    void DeleteCategory(string categoryName);
}