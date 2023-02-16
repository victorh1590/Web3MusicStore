using Microsoft.EntityFrameworkCore;

namespace Web3MusicStore.API.Data.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    public UnitOfWork(DbContext context)
    { 
        _context = context;
    }
    
    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Rollback()
    {
    }
}