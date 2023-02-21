namespace Web3MusicStore.API.Data.UnitOfWork;

internal class StoreUnitOfWork : IUnitOfWork
{
    private readonly StoreDbContext _context;
    public StoreUnitOfWork(StoreDbContext context)
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