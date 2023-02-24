using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;
using CommunityToolkit.Diagnostics;

namespace Web3MusicStore.API.Data.Repositories;

public partial class StoreRepository : IAlbumRepository
{
    public async Task<IReadOnlyCollection<Album>> GetAlbumPagesAsync(int pageNumber = 1, int? userId = null)
    {
        Guard.IsGreaterThanOrEqualTo(pageNumber, 1);
        var query = _context.Albums.AsQueryable();
        if (userId != null) query = query.Where(item => userId == item.UserId);
        return await query.Skip(PageSkip(pageNumber)).Take(PageSize).ToListAsync();
    }

    public async Task<IReadOnlyCollection<Album>> GetRandomAlbumPagesAsync()
    {
        const int pageCount = 5;
        var rand = new Random(Guid.NewGuid().GetHashCode());
        return await _context.Albums
            .AsNoTracking()
            .OrderBy(item => rand.Next())
            .Take(PageSize * pageCount)
            .ToListAsync();
    }

    public async Task<Album?> FindAlbumById(int albumId)
    {
        return await _context.Albums.FindAsync(albumId);
    }

    public async Task InsertAlbumAsync(Album album)
    {
        await _context.Albums.AddAsync(album);
    }

    public void UpdateAlbum(Album album)
    {
        _context.Albums.Update(album);
    }

    public void RemoveAlbum(Album album)
    {
        _context.Albums.Remove(album);
    }

    // public async Task RemoveAlbumByIdAsync(int albumId)
    // {
    //     var album = await _context.Albums.FindAsync(albumId);
    //     Guard.IsNotNull(album);
    //     _context.Albums.Remove(album);
    // }

    public void RemoveAlbumRange(IEnumerable<Album> albums)
    {
        var albumArray = albums as Album[] ?? Array.Empty<Album>();
        Guard.IsNotEmpty(albumArray);
        _context.Albums.RemoveRange(albums);
    }
}