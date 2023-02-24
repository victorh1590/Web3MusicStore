using Microsoft.AspNetCore.Mvc;
using Moq;
using Web3MusicStore.API.Controllers;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.UnitTests;

public partial class AlbumControllerTests
{
    [Test]     
    public async Task GetAlbumById_Returns_Ok_When_Album_Found()
    {
        // Arrange         
        _repository.Setup(repo => repo.FindAlbumById(5)).ReturnsAsync(new Album { Id = 5, Name = "Test Album" });         
        _controller = new AlbumController(_repository.Object, _unitOfWork.Object);                  

        // Act
        var result = await _controller.GetAlbumById(5);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<OkObjectResult>());

        var okResult = result as OkObjectResult;
        var album = okResult?.Value as Album;
        Assert.Multiple(() =>
        {
            Assert.That(album?.Id, Is.EqualTo(5));
            Assert.That(album?.Name, Is.EqualTo("Test Album"));
        });
    }
    
    [Test]     
    public async Task GetAlbumById_Returns_NotFound_When_Id_Invalid()
    {
        // Arrange         
        _repository.Setup(repo => repo.FindAlbumById(-1)).ReturnsAsync((Album?)null);  
        _controller = new AlbumController(_repository.Object, _unitOfWork.Object);

        // Act         
        var result = await _controller.GetAlbumById(-1);          

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }
}