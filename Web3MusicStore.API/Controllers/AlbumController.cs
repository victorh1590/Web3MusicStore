using CommunityToolkit.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Data.UnitOfWork;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AlbumController(IAlbumRepository albumRepository, IUnitOfWork unitOfWork)
    {
        _albumRepository = albumRepository;
        _unitOfWork = unitOfWork;
    }
    
    //TODO Replace return with DTOs.
    // GET
    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetAlbums()
    {
        var albums = await _albumRepository.GetRandomAlbumPagesAsync();
        return Ok(albums);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> GetAlbumById(int id)
    {
        var album = await _albumRepository.FindAlbumById(id);
        if (album != null) return Ok(album);
        return NotFound();
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> PostAlbum(
        [FromBody]Album album)
    {
        if (!ModelState.IsValid) return BadRequest();
        //TODO add DTO conversion here.
        await _albumRepository.InsertAlbumAsync(album);
        // try
        // {
        _unitOfWork.Commit();
        return CreatedAtAction(nameof(GetAlbumById), new { id = album.Id }, album);
        // }
        // catch(Exception ex) 
        //     when(ex is DbUpdateException or DbUpdateConcurrencyException or OperationCanceledException)
        // {
        // }
        //
        // return BadRequest();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteAlbum(int id)
    {
        var album = await _albumRepository.FindAlbumById(id);
        if (album == null) return NotFound();
        _albumRepository.RemoveAlbum(album);
        _unitOfWork.Commit();
        return NoContent();
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult> UpdateAlbum(int id, 
        [FromBody] Album album)
    {
        Guard.IsTrue(album.Id == id);
        var item = await _albumRepository.FindAlbumById(id);
        if (item == null) return NotFound();
        _albumRepository.UpdateAlbum(album);
        _unitOfWork.Commit();
        return NoContent();
    }
}