using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class ListAllCategoriesUseCase(ICategoryService categoryService)
{
    public async Task<List<Category>> Execute()
    {
        return await categoryService.GetAllCategories();
    }
}