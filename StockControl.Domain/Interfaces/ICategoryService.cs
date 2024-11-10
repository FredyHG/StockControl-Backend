using StockControl.Domain.Entities;

namespace StockControl.Domain.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
}