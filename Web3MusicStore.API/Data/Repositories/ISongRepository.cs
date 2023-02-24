using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public interface ISongRepository
{
    Task<IReadOnlyCollection<Song>> GetSongPagesByUserAsync(
        int userId,
        int pageNumber = 1);
    Task<IReadOnlyCollection<Song>> GetSongsByAlbumAsync(int albumId);
    Task<Song?> FindSongById(int songId);
    Task InsertSongAsync(Song song);
    void UpdateSong(Song song);
    void RemoveSong(Song song);
    Task RemoveSongByIdAsync(int songId);
    void RemoveSongRange(IEnumerable<Song> songs);
}