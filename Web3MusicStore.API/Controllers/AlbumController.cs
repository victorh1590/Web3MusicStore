using Microsoft.AspNetCore.Mvc;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    
    private readonly ILogger<AlbumController> _logger;

    private readonly IRepository<Album> _albumRepository;

    public AlbumController(ILogger<AlbumController> logger, IRepository<Album> albumRepository)
    {
        _logger = logger;
        _albumRepository = albumRepository;
    }
    
    //TODO Replace return with DTOs.
    // GET
    [HttpGet]
    [Route("")]
    public async IAsyncEnumerable<Album> GetAlbums()
    {
        await foreach (var album in _albumRepository.GetAllAsAsyncEnumerable())
        {
            yield return album;
        }
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Album>> GetAlbumById(int id)
    {
        var queryResult = await _albumRepository.GetAsync(item => item.Id == id);
        var album = queryResult?.FirstOrDefault();
        if (album != null)
        {
            return Ok(album);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Album>> PostAlbum(Album album)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        //TODO add DTO conversion here.
        await _albumRepository.AddAsync(album);
        if (await _albumRepository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetAlbumById), new { id = album.Id }, album);
        }

        return BadRequest();
    }
}