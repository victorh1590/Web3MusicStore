using System.Linq.Expressions;

namespace Web3MusicStore.API.Data.Repositories;

public interface IRepository<T> where T : class
{
  Task<IEnumerable<T>?> FindAsync(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    string props = "",
    int pageNumber = 1,
    int pageSize = 20);
  
  Task AddAsync(T entity);
  void Delete(T entity);
  void Delete(object? id);
}