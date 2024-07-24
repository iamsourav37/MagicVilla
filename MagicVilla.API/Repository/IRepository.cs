using System.Linq.Expressions;

namespace MagicVilla.API.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<bool> RemoveAsync(T entity);
        Task SaveAsync();
    }
}
