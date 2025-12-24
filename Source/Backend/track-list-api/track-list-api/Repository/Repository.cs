using System.Linq.Expressions;
using api.DbContext;
using api.Models;
using api.Repository.IReposotory;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly TrackListDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(TrackListDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        var item = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        string? includeProperties = null)
    {
        var query = _dbSet.AsQueryable();
        if (filter is not null) query = query.Where(filter);
        if (includeProperties is not null)
            query = includeProperties
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty)
                );
        var result = await query.ToListAsync();
        return result;
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        var query = _dbSet.AsQueryable();
        query = query.Where(filter);

        if (includeProperties is not null)
            query = includeProperties
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty)
                );
        var result = await query.FirstOrDefaultAsync();
        return result;
    }

    public async Task Remove(T entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}