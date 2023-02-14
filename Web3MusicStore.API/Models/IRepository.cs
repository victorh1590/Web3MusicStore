using System.Linq.Expressions;

namespace Web3MusicStore.API.Models;

public interface IRepository<T> where T : class
{
  Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
 IAsyncEnumerable<T> GetAllAsync();
  Task AddAsync(T entity);
  void Delete(T entity);
  void Delete(object? id);
  void Update(T entity);
  protected Task<bool> SaveChangesAsync();
}