namespace Web3MusicStore.API.Data.Repositories;

public partial class StoreRepository
{
    private readonly IStoreDbContext _context;
    private const int PageSize = 20;
    private int PageSkip(int pageNumber) => (pageNumber - 1) * PageSize;
    
    public StoreRepository(IStoreDbContext context)
    {
        _context = context;
    }

}