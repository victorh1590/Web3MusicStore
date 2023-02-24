using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public interface IAlbumRepository
{
    Task<IReadOnlyCollection<Album>> GetAlbumPagesAsync(
        int pageNumber = 1,
        int? userId = null);
    Task<IReadOnlyCollection<Album>> GetRandomAlbumPagesAsync();
    Task<Album?> FindAlbumById(int albumId);
    Task InsertAlbumAsync(Album album);
    void UpdateAlbum(Album album);
    void RemoveAlbum(Album album);
    // Task RemoveAlbumByIdAsync(int albumId);
    void RemoveAlbumRange(IEnumerable<Album> albums);
}