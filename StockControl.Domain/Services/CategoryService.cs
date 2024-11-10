using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;

namespace StockControl.Domain.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<Category>> GetAllCategories()
    {
        return await categoryRepository.GetAllAsync();
    }
}