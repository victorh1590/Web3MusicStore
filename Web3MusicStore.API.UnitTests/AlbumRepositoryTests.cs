using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Models;
using Moq;
using Web3MusicStore.API.Data;
using MockQueryable.Moq;

namespace Web3MusicStore.API.UnitTests;

[TestFixture]
public class AlbumRepositoryTests
{
    private IAlbumRepository _repository = default!;
    private Mock<IStoreDbContext> _mockDbContext = default!;

    [SetUp]
    public void Setup()
    {
        _mockDbContext = new Mock<IStoreDbContext>();
    }

    [Test, Sequential]
    public async Task GetPagesAsync_Returns_Expected_Albums_For_Page_And_User(
        [Values(1, 2, 3, 4, 5, 6, 7)] int userId, 
        [Values(1, 1, 1, 1, 1, 1, 2)] int pageNumber, 
        [Values( new[] {1, 2, 4, 5, 11, 14}, 
            new[] {3, 8, 15},
            new[] {6, 16},
            new[] {7, 24},
            new[] {9, 25},
            new[] {10, 19},
            new int[] {}
            )
        ]
        int[] expectedAlbumIds)
    {
        List<Album> albums = new()
        {
            new() { Id = 1, UserId = 1 }, new() { Id = 2, UserId = 1 }, new() { Id = 3, UserId = 2 }, new() { Id = 4, UserId = 1 }, new() { Id = 5, UserId = 1 },
            new() { Id = 6, UserId = 3 }, new() { Id = 7, UserId = 4 }, new() { Id = 8, UserId = 2 }, new() { Id = 9, UserId = 5 }, new() { Id = 10, UserId = 6 },
            new() { Id = 11, UserId = 1 }, new() { Id = 12, UserId = 7 }, new() { Id = 13, UserId = 8 }, new() { Id = 14, UserId = 1 }, new() { Id = 15, UserId = 2 },
            new() { Id = 16, UserId = 3 }, new() { Id = 17, UserId = 9 }, new() { Id = 18, UserId = 10 }, new() { Id = 19, UserId = 6 }, new() { Id = 20, UserId = 11 },
            new() { Id = 21, UserId = 12 }, new() { Id = 22, UserId = 13 }, new() { Id = 23, UserId = 14 }, new() { Id = 24, UserId = 4 }, new() { Id = 25, UserId = 5 }
        };
        // Arrange
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(albums.AsQueryable().BuildMockDbSet().Object);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var result = await _repository.GetPagesAsync(pageNumber, userId);

        // Assert
        Assert.That(result, Has.Count.EqualTo(expectedAlbumIds.Length));
        CollectionAssert.AreEquivalent(result.Select(a => a.Id).ToArray(), expectedAlbumIds);
    }
    
    [Test, Sequential]
    public async Task GetPagesAsync_Returns_Expected_Album_Pages_When_UserId_Not_Specified(
        [Values(1, 2, 3, 0)] int pageNumber)
    {
        var albums = new List<Album>(
            Enumerable
                .Range(1, 25)
                .Select(n => new Album { Id = n, UserId = n })
            );
        
        var expectedAlbums = pageNumber switch
        {
            1 => albums.Take(20).ToList(),
            2 => albums.Skip(20).ToList(),
            _ => new List<Album>()
        };

        // Arrange
        _mockDbContext
            .Setup(context => context.Albums)
            .Returns(albums.AsQueryable().BuildMockDbSet().Object);
        _repository = new AlbumRepository(_mockDbContext.Object);

        // Act
        var result = await _repository.GetPagesAsync(pageNumber);

        // Assert
        Assert.That(result, Has.Count.EqualTo(expectedAlbums.Count));
        CollectionAssert.AreEquivalent(result.Select(a => a).ToList(), expectedAlbums);
    }
}
