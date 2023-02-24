using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Models;
using CommunityToolkit.Diagnostics;

namespace Web3MusicStore.API.Data.Repositories;

public class AlbumRepository : IAlbumRepository
{
    private readonly IStoreDbContext _context;
    private const int PageSize = 20;
    private int PageSkip(int pageNumber) => (pageNumber - 1) * PageSize;

    public AlbumRepository(IStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Album>> GetPagesAsync(int pageNumber = 1, int? userId = null)
    {
        Guard.IsGreaterThanOrEqualTo(pageNumber, 1);
        var query = _context.Albums.AsQueryable();
        if (userId != null) query = query.Where(item => userId == item.UserId);
        return await query.Skip(PageSkip(pageNumber)).Take(PageSize).ToListAsync();
    }

    public async Task<IReadOnlyCollection<Album>> GetRandomPagesAsync()
    {
        const int pageCount = 5;
        var rand = new Random(Guid.NewGuid().GetHashCode());
        return await _context.Albums
            .AsNoTracking()
            .OrderBy(item => rand.Next())
            .Take(PageSize * pageCount)
            .ToListAsync();
    }

    public async Task<Album?> FindById(int albumId = 0)
    {
        return await _context.Albums.FindAsync(albumId);
    }

    public async Task InsertAsync(Album album)
    {
        await _context.Albums.AddAsync(album);
    }

    public void Update(Album album)
    {
        _context.Albums.Update(album);
    }

    public void Remove(Album album)
    {
        _context.Albums.Remove(album);
    }

    // public async void RemoveByIdAsync(int albumId)
    // {
    //     var album = await _context.Albums.FindAsync(albumId);
    //     if (album != null) _context.Albums.Remove(album);
    // }

    public void RemoveRange(IEnumerable<Album> albums)
    {
        _context.Albums.RemoveRange(albums);
    }
}