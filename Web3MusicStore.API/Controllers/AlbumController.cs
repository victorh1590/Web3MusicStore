using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Data.UnitOfWork;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumController(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }
    
    //TODO Replace return with DTOs.
    // GET
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAlbums()
    {
        var albums = await _albumRepository.GetRandomPagesAsync();
        return Ok(albums);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> GetAlbumById(int id)
    {
        var album = await _albumRepository.FindById(id);
        if (album != null) return Ok(album);
        return NotFound();
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Album>> PostAlbum(Album album,
        [FromServices]IUnitOfWork unityOfWork)
    {
        if (!ModelState.IsValid) return BadRequest();
        //TODO add DTO conversion here.
        await _albumRepository.InsertAsync(album);
        try
        {
            unityOfWork.Commit();
            return CreatedAtAction(nameof(GetAlbumById), new { id = album.Id }, album);
        }
        catch(Exception ex) 
            when(ex is DbUpdateException or DbUpdateConcurrencyException or OperationCanceledException)
        {
        }

        return BadRequest();
    }
}