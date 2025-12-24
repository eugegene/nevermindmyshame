using api.Models;
using System.Linq.Expressions;

namespace api.Repository.IReposotory
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<T> AddAsync(T entity);
        Task Remove(T entity);
    }
}
