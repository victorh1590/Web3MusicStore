using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Web3MusicStore.API.Data.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly ILogger<RepositoryBase<T>> _logger;
    private const int QueryLimit = 100;

    protected RepositoryBase(DbContext context, ILogger<RepositoryBase<T>> logger)
    {
        _dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>?> FindAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string props = "",
        int pageNumber = 1,
        int pageSize = 1)
    {
        IQueryable<T> query = _dbSet;
        // return await DbSet.FirstOrDefaultAsync(predicate);
        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var prop 
                 in props.Split(",", StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(prop);
        }
        
        _logger.LogInformation(message: $"Obtained {query} through {query.Expression.ToString()}", query);
        
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        
        //Pagination
        if (!(pageSize > QueryLimit))
        {
            var skip = (pageNumber - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            return await query.ToListAsync();
        }
        
        return null;
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
    
    public virtual void Delete(object? id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null) Delete(entity);
    }

    // public async Task<bool> SaveChangesAsync()
    // {
    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //         return true;
    //     }
    //     catch (DbUpdateConcurrencyException ex)
    //     {
    //         _logger.LogError(ex, "A concurrency error occurred while saving changes to the database: {Message}", ex.Message);
    //     }
    //     catch (DbUpdateException ex)
    //     {
    //         _logger.LogError(ex, "An error occurred while saving changes to the database: {Message}", ex.Message);
    //     }
    //     catch (OperationCanceledException ex)
    //     {
    //         _logger.LogError(ex, "An operation was cancelled while saving changes to the database: {Message}", ex.Message);
    //     }
    //     return false;
    // }
}