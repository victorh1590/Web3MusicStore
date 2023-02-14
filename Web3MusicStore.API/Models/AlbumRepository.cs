namespace Web3MusicStore.API.Models;

public class AlbumRepository : RepositoryBase<Album>
{
  public AlbumRepository(StoreDbContext context, ILogger<RepositoryBase<Album>> logger) : base(context, logger)
  {
  }
}