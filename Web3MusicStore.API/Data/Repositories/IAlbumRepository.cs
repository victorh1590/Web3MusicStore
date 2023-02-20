using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public interface IAlbumRepository
{
    Task<IReadOnlyCollection<Album>> GetPagesAsync(
        int pageNumber = 1,
        int? userId = null);
    Task<IReadOnlyCollection<Album>> GetRandomPagesAsync(
        int pageNumber = 1,
        Guid? guid = null
        );
    Task<Album?> FindById(int albumId = 0);
    Task InsertAsync(Album album);
    void Update(Album album);
    void Remove(Album album);
    void RemoveByIdAsync(int albumId);
    void RemoveRange(IEnumerable<Album> albums);
}