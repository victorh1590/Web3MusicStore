using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;

namespace Web3MusicStore.API.UnitTests;

public partial class AlbumRepositoryTests
{
    [Test, Sequential]
    public async Task GetRandomPagesAsync_Always_Returns_Same_Page_Given_GUID(
        [Values(2)] int pageNumber)
    {
        var exampleGuid = new Guid("8cdaa2d0-f7a7-4635-9a44-5a941d9282af");
        Console.WriteLine(exampleGuid);
        Console.WriteLine(exampleGuid.GetHashCode());
        List<Album> albums = new()
        {
            new() { Id = 1, UserId = 1 }, new() { Id = 2, UserId = 1 }, new() { Id = 3, UserId = 2 },
            new() { Id = 4, UserId = 1 }, new() { Id = 5, UserId = 1 },
            new() { Id = 6, UserId = 3 }, new() { Id = 7, UserId = 4 }, new() { Id = 8, UserId = 2 },
            new() { Id = 9, UserId = 5 }, new() { Id = 10, UserId = 6 },
            new() { Id = 11, UserId = 1 }, new() { Id = 12, UserId = 7 }, new() { Id = 13, UserId = 8 },
            new() { Id = 14, UserId = 1 }, new() { Id = 15, UserId = 2 },
            new() { Id = 16, UserId = 3 }, new() { Id = 17, UserId = 9 }, new() { Id = 18, UserId = 10 },
            new() { Id = 19, UserId = 6 }, new() { Id = 20, UserId = 11 },
            new() { Id = 21, UserId = 12 }, new() { Id = 22, UserId = 13 }, new() { Id = 23, UserId = 14 },
            new() { Id = 24, UserId = 4 }, new() { Id = 25, UserId = 5 }
        };

        // Arrange
        var dbSet = albums.AsQueryable().BuildMockDbSet().Object;
        
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(dbSet);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var expectedResults = await _repository.GetRandomPagesAsync(pageNumber, exampleGuid);
        var result = await _repository.GetRandomPagesAsync(pageNumber, exampleGuid);

        // Assert
        Assert.That(expectedResults, Has.Count.EqualTo(5));
        Assert.That(result, Has.Count.EqualTo(expectedResults.Count));
        CollectionAssert.AreEquivalent(result.Select(a => a.Id).ToArray(), 
            expectedResults.Select(a => a.Id).ToArray());
    }
}