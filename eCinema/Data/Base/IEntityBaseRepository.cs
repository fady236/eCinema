using eCinema.Models;
using System.Linq.Expressions;

namespace eCinema.Data.Base
{
    public interface IEntityBaseRepository<T>where T : class , IEntityBase ,new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity); // ✅ لا تنسى إرجاع `Task<T>`
        Task<bool> DeleteAsync(int id);
    }
}
