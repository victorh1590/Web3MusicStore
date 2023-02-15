using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public interface IAlbumRepository : IRepository<Album>
{ 
    IAsyncEnumerable<Album> GetRandomEntriesAsync(int numEntries);
}