namespace Web3MusicStore.API.Models;

public class StoreRepository : IStoreRepository {
  private StoreDbContext _context;

  StoreRepository(StoreDbContext context) {
    _context = context;
  }

  public IQueryable<Album> Albums => _context.Albums;
  public IQueryable<Artist> Artists => _context.Artists;
  public IQueryable<Song> Songs => _context.Songs;
}