using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public class AlbumRepository : RepositoryBase<Album>, IAlbumRepository
{
  public AlbumRepository(DbContext context, ILogger<RepositoryBase<Album>> logger) : base(context, logger)
  {
  }

  public async IAsyncEnumerable<Album> GetRandomEntriesAsync(int numEntries = 1)
  {
    var albums = await FindAsync(
      pageSize: numEntries, 
      orderBy: query => query.OrderBy(item => Guid.NewGuid()));
    if (albums == null) yield break;
    {
      foreach (var item in albums)
      {
        yield return item;
      }
    }
  }
}