using Microsoft.AspNetCore.Mvc;
using Web3MusicStore.API.Models;
using System.Linq;

namespace Web3MusicStore.API.Controllers;

public class MusicStoreController : Controller
{
    
    private readonly ILogger<MusicStoreController> _logger;

    private readonly IRepository<Album> _albumRepository;

    public MusicStoreController(ILogger<MusicStoreController> logger, IRepository<Album> albumRepository)
    {
        _logger = logger;
        _albumRepository = albumRepository;
    }
    
    //TODO Replace return with DTOs.
    // GET
    [HttpGet(Name = "GetAllAlbums")]
    public async IAsyncEnumerable<Album> GetAlbums()
    {
        await foreach (var album in _albumRepository.GetAllAsync())
        {
            yield return album;
        }
    }
}