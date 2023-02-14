using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Web3MusicStore.API.Models;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly StoreDbContext _context;
    private readonly ILogger<RepositoryBase<T>> _logger;
    private IQueryable<T> DbSet => _context.Set<T>();

    protected RepositoryBase(StoreDbContext context, ILogger<RepositoryBase<T>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    // public virtual async Task<IEnumerable<T>> GetAllAsync()
    // {
    //     return await DbSet.ToListAsync();
    // }
    public virtual IAsyncEnumerable<T> GetAllAsync()
    {
        return DbSet.AsAsyncEnumerable();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    
    public virtual void Delete(object? id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null) Delete(entity);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    async Task<bool> IRepository<T>.SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "A concurrency error occurred while saving changes to the database: {Message}", ex.Message);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while saving changes to the database: {Message}", ex.Message);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogError(ex, "An operation was cancelled while saving changes to the database: {Message}", ex.Message);
        }
        return false;
    }
}