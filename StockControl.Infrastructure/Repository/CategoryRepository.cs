using StockControl.Domain.Entities;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class CategoryRepository(AppDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{
}