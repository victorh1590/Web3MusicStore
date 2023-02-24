using CommunityToolkit.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Data.Repositories;

public partial class StoreRepository : ISongRepository
{
    public async Task<IReadOnlyCollection<Song>> GetSongPagesByUserAsync(int userId, int pageNumber = 1)
    {
        Guard.IsGreaterThanOrEqualTo(pageNumber, 1);
        Guard.IsNotNull(await _context.User.FindAsync(userId));
        var query = _context.Albums
            .Where(album => album.UserId == userId && album.Songs.Any())
            .Include(album => album.Name)
            .SelectMany(album => album.Songs)
            .Skip(PageSkip(pageNumber))
            .Take(PageSize);

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyCollection<Song>> GetSongsByAlbumAsync(int albumId)
    {
        Guard.IsNotNull(await _context.Albums.FindAsync(albumId));
        return await _context.Songs
            .Where(song => song.AlbumId == albumId)
            .ToListAsync();
    }

    public async Task<Song?> FindSongById(int songId)
    {
        return await _context.Songs.FindAsync(songId);
    }

    public async Task InsertSongAsync(Song song)
    {
        await _context.Songs.AddAsync(song);
    }

    public void UpdateSong(Song song)
    {
        _context.Songs.Update(song);
    }

    public void RemoveSong(Song song)
    {
        _context.Songs.Remove(song);
    }

    public async Task RemoveSongByIdAsync(int songId)
    {
        var song = await _context.Songs.FindAsync(songId);
        Guard.IsNotNull(song);
        _context.Songs.Remove(song);
    }

    public void RemoveSongRange(IEnumerable<Song> songs)
    {
        var songArray = songs as Song[] ?? Array.Empty<Song>();
        Guard.IsNotEmpty(songArray.ToArray());
        _context.Songs.RemoveRange(songArray.ToArray());
    }
}