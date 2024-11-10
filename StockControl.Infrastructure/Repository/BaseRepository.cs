using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T>
    where T : class
{
    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<T>> AddAllAsync(List<T> listEntity)
    {
        await context.Set<T>().AddRangeAsync(listEntity);
        await context.SaveChangesAsync();

        return listEntity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<T>> UpdateAllAsync(List<T> entity)
    {
        foreach (var ent in entity) context.Entry(ent).State = EntityState.Modified;

        await context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Deleted;
        await context.SaveChangesAsync();
    }

    public async Task<T> FindByAsync(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = context.Set<T>();

        if (include != null) query = include(query);

        query = query.AsNoTracking();

        return await query.Where(expression).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        var query = context.Set<T>().AsSplitQuery();

        if (include is not null)
            query = include(query);

        return await query.ToListAsync();
    }
}