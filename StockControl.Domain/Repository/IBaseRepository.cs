using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace StockControl.Domain.Repository;

public interface IBaseRepository<T>
{
    Task<T> AddAsync(T entity);
    Task<List<T>> AddAllAsync(List<T> listEntity);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> UpdateAllAsync(List<T> entity);
    Task DeleteAsync(T entity);

    Task<T> FindByAsync(Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
}