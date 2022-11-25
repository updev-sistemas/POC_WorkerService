using OrderManager.Entity;
using System.Linq.Expressions;

namespace OrderManager.Database.Contracts
{
    public interface IDefaultRepository<T> where T : IEntity
    {
        Task<T?> FindByIdAsync(string id);
        Task<List<T>>? FindAllAsync();
        Task<List<T>>? QueryAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T target);
        Task UpdateAsync(string id, T target);
        Task RemoveAsync(string id);
    }
}
